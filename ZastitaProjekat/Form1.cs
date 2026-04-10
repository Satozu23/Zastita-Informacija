using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace ZastitaProjekat
{
    public partial class Form1 : Form
    {
        private TcpListener serverListener;
        private Thread serverThread;
        private bool isServerRunning = false;

        private FileSystemWatcher watcher;

        public Form1()
        {
            InitializeComponent();
            txtMyIP.Text = GetLocalIPAddress();
        }

        //Izaberi Folder za praćenje
        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Izaberi folder koji želiš da pratiš";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtFolderPath.Text = dialog.SelectedPath;
                    Log("Izabran folder: " + dialog.SelectedPath);
                }
            }
        }
        //Izaberi folder za čuvanje
        private void btnSelectOutput_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Izaberi gde će se čuvati enkriptovani fajlovi";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtOutputPath.Text = dialog.SelectedPath;
                    Log("Izabran Output folder: " + dialog.SelectedPath);
                }
            }
        }
        //Pokreni server
        private void btnStartServer_Click(object sender, EventArgs e)
        {
            if (!isServerRunning)
            {
                int port;
                if (!int.TryParse(txtPort.Text, out port))
                {
                    MessageBox.Show("Unesi validan port.");
                    return;
                }

                isServerRunning = true;
                btnStartServer.Text = "Zaustavi Server";
                lblServerStatus.Text = "Status: Čekam konekciju...";
                lblServerStatus.ForeColor = System.Drawing.Color.Green;

                serverThread = new Thread(() => RunServer(port));
                serverThread.IsBackground = true;
                serverThread.Start();
            }
            else
            {
                isServerRunning = false;
                try { serverListener.Stop(); } catch { }

                btnStartServer.Text = "Pokreni Server";
                lblServerStatus.Text = "Status: Isključen";
                lblServerStatus.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void RunServer(int port)
        {
            try
            {
                serverListener = new TcpListener(IPAddress.Any, port);
                serverListener.Start();

                while (isServerRunning)
                {
                    TcpClient client = serverListener.AcceptTcpClient();

                    this.Invoke(new Action(() => Log(">>> NEKO SE POVEZAO! Primam podatke...")));

                    using (NetworkStream stream = client.GetStream())
                    {
                        string receivedFileName = Path.Combine(txtOutputPath.Text, $"Received_{DateTime.Now.Ticks}.enc");

                        using (FileStream fs = File.Create(receivedFileName))
                        {
                            stream.CopyTo(fs);
                        }

                        this.Invoke(new Action(() =>
                        {
                            Log(">>> FAJL PRIMLJEN: " + receivedFileName);
                            ProcessReceivedFile(receivedFileName);
                        }));
                    }
                    client.Close();
                }
            }
            catch (Exception ex)
            {
                if (isServerRunning)
                {
                    this.Invoke(new Action(() => Log("Greska servera: " + ex.Message)));
                }
            }
        }
        //Izaberi fajl za slanje
        private void btnSelectFileToSend_Click(object sender, EventArgs e)
        {
            string targetIP = txtTargetIP.Text;
            int port = 5000;
            int.TryParse(txtPort.Text, out port);

            if (string.IsNullOrEmpty(targetIP))
            {
                MessageBox.Show("Unesi IP adresu kolege!");
                return;
            }

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Encrypted files (*.enc)|*.enc";
                ofd.Title = "Izaberi fajl za slanje";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string filePath = ofd.FileName;

                    Thread sendThread = new Thread(() =>
                    {
                        try
                        {
                            this.Invoke(new Action(() => Log($"Povezujem se na {targetIP}:{port}...")));

                            using (TcpClient client = new TcpClient())
                            {
                                client.Connect(targetIP, port);

                                using (NetworkStream stream = client.GetStream())
                                {
                                    byte[] fileBytes = File.ReadAllBytes(filePath);
                                    stream.Write(fileBytes, 0, fileBytes.Length);
                                }
                            }

                            this.Invoke(new Action(() => Log("USPEŠNO POSLATO!")));
                            MessageBox.Show("Fajl je poslat!");
                        }
                        catch (Exception ex)
                        {
                            this.Invoke(new Action(() => Log("Greška pri slanju: " + ex.Message)));
                            MessageBox.Show("Nije uspelo slanje");
                        }
                    });
                    sendThread.Start();
                }
            }
        }

        //CheckBox (FSW)
        private void chkEnableFSW_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEnableFSW.Checked)
            {

                if (string.IsNullOrEmpty(txtFolderPath.Text) || !Directory.Exists(txtFolderPath.Text))
                {
                    MessageBox.Show("Izaberite validan folder pre uključivanja.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    chkEnableFSW.Checked = false;
                    return;
                }

                startWatching();
            }
            else
            {
                stopWatching();
            }
        }
        private void ProcessReceivedFile(string encFilePath)
        {
            try
            {
                Log("--- ZAPOČINJEM OBRADU PRIMLJENOG FAJLA ---");

                byte[] package = File.ReadAllBytes(encFilePath);

                int headerLength = BitConverter.ToInt32(package, 0);

                byte[] headerBytes = new byte[headerLength];
                Array.Copy(package, 4, headerBytes, 0, headerLength);
                string jsonHeader = Encoding.UTF8.GetString(headerBytes);

                FileMetadata meta = FileMetadata.FromJson(jsonHeader);
                Log($"Header Info -> Ime: {meta.FileName}, Algo: {meta.EncryptionAlgorithm}, Hash: {meta.TigerHashSignature}");

                int contentOffset = 4 + headerLength;
                int contentLength = package.Length - contentOffset;
                byte[] encryptedContent = new byte[contentLength];
                Array.Copy(package, contentOffset, encryptedContent, 0, contentLength);

                byte[] decryptedBytes = null;

                string rawKey = txtKey.Text.PadRight(16, ' ').Substring(0, 16);
                byte[] key = Encoding.UTF8.GetBytes(rawKey);

                string rawIV = txtIV.Text.PadRight(16, ' ').Substring(0, 16);
                byte[] iv = Encoding.UTF8.GetBytes(rawIV);

                int railKeys = (int)numRailfence.Value;

                if (meta.EncryptionAlgorithm.Contains("Railfence"))
                {
                    decryptedBytes = CryptoAlgorithms.RailFenceDecrypt(encryptedContent, railKeys);
                }
                else if (meta.EncryptionAlgorithm.Contains("XXTEA"))
                {
                    byte[] decryptedWithPad = CryptoAlgorithms.XXTEA_CBC_Decrypt(encryptedContent, key, iv);
                    decryptedBytes = new byte[meta.FileSize];
                    Array.Copy(decryptedWithPad, decryptedBytes, meta.FileSize);
                }

                Log("Računam hash dekriptovanog fajla...");
                byte[] calculatedHashBytes = Tiger.ComputeHash(decryptedBytes);
                string calculatedHash = BitConverter.ToString(calculatedHashBytes).Replace("-", "");

                if (calculatedHash == meta.TigerHashSignature)
                {
                    Log("INTEGRITET POTVRĐEN! Hash se poklapa.");
                    Log("FILE IS VALID.");

                    string originalSavePath = Path.Combine(txtOutputPath.Text, "DECRYPTED_" + meta.FileName);
                    File.WriteAllBytes(originalSavePath, decryptedBytes);
                    Log("Sačuvan dekriptovan fajl: " + originalSavePath);
                    MessageBox.Show("Primljen fajl je validan i sačuvan!", "Uspeh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Log("GREŠKA! HASH SE NE POKLAPA!");
                    Log($"Očekivano: {meta.TigerHashSignature}");
                    Log($"Dobijeno : {calculatedHash}");
                    MessageBox.Show("Fajl je oštećen ili izmenjen! Hash ne odgovara.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                Log("Greska pri dekripciji: " + ex.Message);
            }
        }
        private string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "127.0.0.1";
        }
        private void startWatching()
        {
            try
            {
                watcher = new FileSystemWatcher();

                watcher.Path = txtFolderPath.Text;

                watcher.Filter = "*.*";

                watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite;

                watcher.Created += OnNewFileDetected;

                watcher.EnableRaisingEvents = true;

                Log("FSW uključen. Pratim folder: " + txtFolderPath.Text);
                txtFolderPath.Enabled = false;
                btnSelectFolder.Enabled = false;
            }
            catch (Exception ex)
            {
                Log("Greška pri pokretanju FSW: " + ex.Message);
            }
        }

        private void stopWatching()
        {
            if (watcher != null)
            {
                watcher.EnableRaisingEvents = false;
                watcher.Dispose();
                watcher = null;
            }

            Log("FSW isključen.");
            txtFolderPath.Enabled = true;
            btnSelectFolder.Enabled = true;
        }

        private void OnNewFileDetected(object sender, FileSystemEventArgs e)
        {
            System.Threading.Thread.Sleep(500);

            this.Invoke(new Action(() =>
            {
                Log("------------------------------------------------");
                Log("DETEKTOVAN NOVI FAJL: " + e.Name);

                if (string.IsNullOrEmpty(txtOutputPath.Text))
                {
                    Log("GRESKA: Nije izabran Output folder! Enkripcija otkazana.");
                    return;
                }

                string selectedAlgo = cmbAlgorithm.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(selectedAlgo))
                {
                    Log("GRESKA: Nije izabran algoritam! Podrazumevam Railfence.");
                    selectedAlgo = "Railfence";
                }

                try
                {
                    byte[] originalBytes = File.ReadAllBytes(e.FullPath);
                    Log($"Učitano {originalBytes.Length} bajtova.");

                    byte[] hashBytes = Tiger.ComputeHash(originalBytes);
                    string hashString = BitConverter.ToString(hashBytes).Replace("-", "");
                    Log("Tiger Hash originala: " + hashString);

                    byte[] encryptedBytes = null;
                    string rawKey = txtKey.Text.PadRight(16, ' ').Substring(0, 16);
                    byte[] key = Encoding.UTF8.GetBytes(rawKey);

                    string rawIV = txtIV.Text.PadRight(16, ' ').Substring(0, 16);
                    byte[] iv = Encoding.UTF8.GetBytes(rawIV);

                    int railKeys = (int)numRailfence.Value;

                    if (selectedAlgo.Contains("Railfence"))
                    {
                        Log($"Enkriptujem sa Railfence (k={railKeys})...");
                        encryptedBytes = CryptoAlgorithms.RailFenceEncrypt(originalBytes, railKeys);
                    }
                    else if (selectedAlgo.Contains("XXTEA"))
                    {
                        Log($"Enkriptujem sa XXTEA-CBC (Key: {rawKey})...");
                        encryptedBytes = CryptoAlgorithms.XXTEA_CBC_Encrypt(originalBytes, key, iv);
                    }

                    var metadata = new FileMetadata
                    {
                        FileName = e.Name,
                        FileSize = originalBytes.Length,
                        CreationTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        EncryptionAlgorithm = selectedAlgo,
                        TigerHashSignature = hashString
                    };

                    string jsonHeader = metadata.ToJson();
                    byte[] headerBytes = Encoding.UTF8.GetBytes(jsonHeader);
                    byte[] headerLengthBytes = BitConverter.GetBytes(headerBytes.Length);


                    using (var memoryStream = new MemoryStream())
                    {
                        memoryStream.Write(headerLengthBytes, 0, headerLengthBytes.Length);
                        memoryStream.Write(headerBytes, 0, headerBytes.Length);
                        memoryStream.Write(encryptedBytes, 0, encryptedBytes.Length);

                        byte[] finalPackage = memoryStream.ToArray();

                        string outputFileName = Path.Combine(txtOutputPath.Text, e.Name + ".enc");
                        File.WriteAllBytes(outputFileName, finalPackage);

                        Log("USPEŠNO! Fajl sačuvan kao: " + outputFileName);
                        Log($"Ukupna veličina paketa: {finalPackage.Length} bajtova.");
                    }
                }
                catch (Exception ex)
                {
                    Log("GREŠKA TOKOM PROCESA: " + ex.Message);
                }
            }));
        }

        private void Log(string message)
        {
            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            rtbLog.AppendText($"[{timestamp}] {message}\n");

            rtbLog.ScrollToCaret();
        }
    }
}
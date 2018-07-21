using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductionMaker
{
    public partial class Main : Form
    {

        public Main()
        {
            InitializeComponent();
        }
        private string pastaAplicacao = Application.StartupPath;
        private DataGridViewCheckBoxColumn c1;
        private CheckBox ckBox;

        #region Manipulação do registro do windows
        private string CaminhoPadrao(string caminho, Utils.Enumeracao.TipoDiretorio tipo)
        {
            string retorno = "";
            string key = "ENTRADA";

            if (tipo == Utils.Enumeracao.TipoDiretorio.GravarSaida || tipo == Utils.Enumeracao.TipoDiretorio.LerSaida)
                key = "SAIDA";

            try
            {
                if (tipo == Utils.Enumeracao.TipoDiretorio.GravarEntrada || tipo == Utils.Enumeracao.TipoDiretorio.GravarSaida)
                    INI.Gravar("PRODUCTION_" + key, key, caminho);
                else
                    retorno = INI.Ler("[PRODUCTION_" + key + "]", key);

                return retorno;

            }
            catch (Exception)
            {
                return "";
                //string erro = ex.Message;
            }
        }
        #endregion


        protected void CarregaCombo()
        {
            ckbcaminho.DropDownStyle = ComboBoxStyle.DropDownList;
            ckbcaminho.DataSource = INI.GetArquivo();
            ckbcaminho.DisplayMember = "Nome";
            ckbcaminho.ValueMember = "Endereco";
        }

        private void Main_Load(object sender, EventArgs e)
        {
            dateTimePicker1.MaxDate = DateTime.Today;
            dateTimePicker1.Value = DateTime.Today.AddDays(-20);
            CarregaCombo();

            ckBox = new CheckBox();
            Rectangle rect = this.dataGridView1.GetCellDisplayRectangle(0, -1, true);
            rect.Y = 4;
            rect.X = rect.Location.X + (rect.Width / 4);
            ckBox.Size = new Size(14, 14);
            ckBox.Location = rect.Location;
            ckBox.CheckedChanged += new EventHandler(ckBox_CheckedChanged);
            this.dataGridView1.Controls.Add(ckBox);
        }

        void ckBox_CheckedChanged(object sender, EventArgs e)
        {
            bool checado = this.ckBox.Checked;
            foreach (DataGridViewRow dg in dataGridView1.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dg.Cells[0];
                chk.Value = checado;
            }

           // dataGridView1.SelectedRows
        }

        protected void Localizar()
        {
            string caminho = ckbcaminho.SelectedValue.ToString();
            if (caminho == "")
            {
                //OpenFileDialog fdlg = new OpenFileDialog();
                FolderBrowserDialog fdlg = new FolderBrowserDialog();
                fdlg.Description = "Selecione a pasta onde estão os arquivos";
                fdlg.ShowNewFolderButton = false;

                //Exibe a caixa de diálogo
                if (fdlg.ShowDialog() == DialogResult.OK)
                {
                    INI.GravaLinha(fdlg.SelectedPath);
                    CarregaCombo();
                    ckbcaminho.SelectedValue = fdlg.SelectedPath;
                    DiretorioOK(true);
                }
            }
            else
            {
                ListarArquivos();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Localizar();
        }

        private void DiretorioOK(bool habilita)
        {
            dateTimePicker1.Enabled = habilita;
            button1.Enabled = habilita;
            //MenuListar.Enabled = habilita;
        }

        private bool Desconsiderar(string extensao)
        {
            bool retorno = !ckbdirecaoextensao.Checked;
            string[] itens = { ".svn-base", ".ini", ".bak", ".db" };

            if (txtextensao.Text != "")
                itens = txtextensao.Text.Split(',');

            for (int i = 0; i < itens.Length; i++)
            {
                if (extensao.Trim().ToLower() == itens[i].ToString().Trim().ToLower())
                    return !retorno;
            }
            return retorno;
        }

        protected void DirSearch(string sDir, List<Models.Arquivo> lista)
        {
            string nome = "";
            try
            {
                //Marca o diretório a ser listado
                DirectoryInfo diretorio = new DirectoryInfo(sDir);

                //Executa função GetFile(Lista os arquivos desejados de acordo com o parametro)
                DirectoryInfo[] pastaBruta = diretorio.GetDirectories();
                List<DirectoryInfo> pastas = pastaBruta.ToList();//.Where(item => item.Attributes == FileAttributes.Directory).ToList();

                if (checkBox1.Checked)
                    dateTimePicker1.Value = Convert.ToDateTime("01/01/1900");

                foreach (DirectoryInfo d in pastas)
                {

                    FileInfo[] filePasta = d.GetFiles("*.*");
                    List<FileInfo> infoPasta = filePasta.Where(item => (item.LastWriteTime >= dateTimePicker1.Value) && (Desconsiderar(item.Extension))).ToList();
                    nome = d.FullName;

                    foreach (FileInfo fileinfo in infoPasta.OrderByDescending(a => a.LastWriteTime))
                    {


                        FileStream fs = new FileStream(fileinfo.DirectoryName + "\\" + fileinfo.Name, FileMode.Open, FileAccess.Read);
                        Encoding r = Utils.Tools.GetFileEncoding(fs);
                        fs.Close();

                        Models.Arquivo arq = new Models.Arquivo();

                        arq.Data = fileinfo.LastWriteTime;
                        arq.FullName = fileinfo.DirectoryName;
                        arq.Name = fileinfo.Name;
                        arq.Encoding = r.BodyName;
                        arq.Tamanho = fileinfo.Length;
                        lista.Add(arq);
                        listResultado.Items.Add(arq.FullName + "\\" + arq.Name + "----" + arq.Data);


                        toolStripStatusLabel1.Text = fileinfo.Name;
                        Application.DoEvents();

                    }
                    DirSearch(nome, lista);
                }

            }
            catch (System.Exception excpt)
            {
                Models.Arquivo arq = new Models.Arquivo();


                arq.Data = DateTime.Now;
                arq.FullName = "";
                arq.Name = excpt.Message;
                arq.Encoding = "Não tem";
                arq.OK = false;
                arq.Tamanho = 0;
                lista.Add(arq);

                listResultado.Items.Add(arq.FullName + "\\" + arq.Name + "----" + arq.Data);

                toolStripStatusLabel1.Text = excpt.Message;
                Application.DoEvents();
                DirSearch(nome, lista);
            }
        }



        private static void ListDirectory(TreeView tr, string path, bool consideraData, DateTime data)
        {
            tr.Nodes.Clear();
            DirectoryInfo rootDirectoryInfo = new DirectoryInfo(path);
            tr.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo, consideraData, data));
        }

        private void CheckAllChildNodes(TreeNode treeNode, bool nodeChecked)
        {
            foreach (TreeNode node in treeNode.Nodes)
            {
                node.Checked = nodeChecked;
                if (node.Nodes.Count > 0)
                {
                    // If the current node has child nodes, call the CheckAllChildsNodes method recursively. 
                    this.CheckAllChildNodes(node, nodeChecked);
                }
            }
        }

        private static TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo, bool consideraData, DateTime data)
        {
            TreeNode directoryNode = new TreeNode(directoryInfo.Name);
            if (consideraData)
            {
                foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
                    directoryNode.Nodes.Add(CreateDirectoryNode(directory, consideraData, data));
                foreach (FileInfo file in directoryInfo.GetFiles())
                    directoryNode.Nodes.Add(new TreeNode(file.Name + " - " + file.LastWriteTime.ToString()));
                return directoryNode;
            }
            else
            {
                foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
                    directoryNode.Nodes.Add(CreateDirectoryNode(directory, consideraData, data));

                foreach (FileInfo file in directoryInfo.GetFiles())
                {
                    data = Convert.ToDateTime(data.ToString("dd/MM/yyyy"));

                    if (file.LastWriteTime >= data)
                    {
                        directoryNode.Nodes.Add(new TreeNode(file.Name + " - " + file.LastWriteTime.ToString()));
                    }
                }

                return directoryNode;
            }
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            // The code only executes if the user caused the checked state to change. 
            if (e.Action != TreeViewAction.Unknown)
            {
                if (e.Node.Nodes.Count > 0)
                {
                    /* Calls the CheckAllChildNodes method, passing in the current 
                    Checked value of the TreeNode whose checked state changed. */
                    this.CheckAllChildNodes(e.Node, e.Node.Checked);
                }
            }
        }

        // METODO RECURSIVO PARA LISTA DIRETORIOS E SUB-DIRETORIOS
        private void ListaDiretorios(DirectoryInfo diretorioPai, TreeNode noPai)
        {
            // para cada sub-diretorio 
            foreach (DirectoryInfo dir in diretorioPai.GetDirectories())
            {
                // adiciona diretorio ao no corrente
                noPai.Nodes.Add(dir.Name);

                // lista diretorios do diretorio corrente
                ListaDiretorios(dir, noPai.LastNode);
            }
        }


        protected void Processa()
        {
            try
            {
                string caminho = ckbcaminho.SelectedValue.ToString();
                listResultado.Items.Clear();
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();                

                toolStripStatusLabel1.Text = "Iniciando busca de arquivos";
                Application.DoEvents();
                button1.Enabled = false;
                button4.Enabled = !button1.Enabled;

                List<Models.Arquivo> lista = new List<Models.Arquivo>();
                DateTime vlPadrao = dateTimePicker1.Value;

                if (checkBox1.Checked)
                    dateTimePicker1.Value = Convert.ToDateTime("01/01/1900");

                ////Marca o diretório a ser listado
                DirectoryInfo diretorio = new DirectoryInfo(@caminho);
                FileInfo[] arquivos = diretorio.GetFiles("*.*");
                List<FileInfo> infoPasta = arquivos.Where(item => (item.LastWriteTime >= dateTimePicker1.Value) && (Desconsiderar(item.Extension))).ToList();

                //ignorando tipos de arquivos
                infoPasta = infoPasta.ToList();

                DirSearch(@caminho, lista);

                //Começamos a listar os arquivos
                foreach (FileInfo fileinfo in infoPasta.OrderByDescending(a => a.LastWriteTime))
                {
                    Models.Arquivo arq = new Models.Arquivo();                   

                    FileStream fs = new FileStream(fileinfo.DirectoryName + "\\" + fileinfo.Name, FileMode.Open, FileAccess.Read);
                    Encoding r = Utils.Tools.GetFileEncoding(fs);
                    fs.Close();

                    arq.Encoding = r.BodyName;


                    arq.Data = fileinfo.LastWriteTime;
                    arq.FullName = fileinfo.DirectoryName;
                    arq.Name = fileinfo.Name;
                    arq.Tamanho = fileinfo.Length;
                    bool pode = true;


                    //----validação no 
                    if (ckcIgEncode.Checked == true)
                    {
                        if (txtEncode.Text != "")
                        {
                            //string xenc;
                            //xenc = txtEncode.Text.Split(',').ToString();

                            string[] result = txtEncode.Text.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

                            foreach (string s in result)
                            {
                                if (s.ToUpper() == arq.Encoding.ToUpper())
                                {
                                    pode = false;
                                }
                            }
                        }
                    }


                    if (!pode)
                    {
                        break;
                    }
                    else
                    {
                        listResultado.Items.Add(arq.FullName + "\\" + arq.Name + "----" + arq.Data);
                        lista.Add(arq);
                    }

                    toolStripStatusLabel1.Text = fileinfo.Name;
                    Application.DoEvents();
                }

                lista = lista.OrderByDescending(c => c.Data).ToList();
                listResultado.Items.Clear();

                if (lista.Count == 0)
                {
                    MessageBox.Show("Não existem arquivos que correspondam a sua pesquisa", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    toolStripStatusLabel1.Text = "Nenhum arquivo";
                    groupBox1.Enabled = false;
                    groupBox2.Enabled = false;
                    groupBox4.Enabled = false;
                    //MenuExportar.Enabled = false;
                }
                else
                {
                    dataGridView1.AutoGenerateColumns = false;
                    dataGridView1.DataSource = lista;


                    foreach (Models.Arquivo file in lista)
                    {
                        if (file.FullName != "")
                        {
                            listResultado.Items.Add(file.FullName + "\\" + file.Name + "----" + file.Data);
                        }
                        else
                        {
                            listResultado.Items.Add(file.FullName + "\\" + file.Name + "----" + file.Data);
                        }
                    }
                    groupBox1.Enabled = true;
                    groupBox2.Enabled = true;
                    groupBox4.Enabled = true;
                    toolStripStatusLabel1.Text = lista.Count.ToString() + " Arquivos afetados";
                    Application.DoEvents();
                    dateTimePicker1.Value = vlPadrao;
                }

                button1.Enabled = true;
                button4.Enabled = !button1.Enabled;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            ListarArquivos();
        }

        private void ListarArquivos()
        {
            button1.Text = "Aguarde processando...";
            button1.Enabled = false;
            Processa();
            button1.Text = "Listar arquivos";
            button1.Enabled = true;
            button5.Enabled = true;
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = !checkBox1.Checked;
        }

        private void txtcaminho_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Tab) || e.KeyCode.Equals(Keys.Enter))
            {
                bool bExistDirectory = Directory.Exists(ckbcaminho.SelectedValue.ToString());
                DiretorioOK(bExistDirectory);

                if (!bExistDirectory)
                {
                    MessageBox.Show("Diretório não localizado, por favor tente novamente", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ckbcaminho.SelectedValue = "";
                }

            }
        }

        private void button2_Leave(object sender, EventArgs e)
        {
            if (ckbcaminho.SelectedValue.ToString() != "")
            {
                if (Directory.Exists(ckbcaminho.SelectedValue.ToString()))
                    DiretorioOK(true);
                else
                {
                    MessageBox.Show("Diretório não localizado, por favor tente novamente", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ckbcaminho.SelectedValue = "";
                    DiretorioOK(false);
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        public void CriaArquivo(string conteudo, string caminho)
        {
            if (caminho != "")
                pastaAplicacao = caminho;

            string path = caminho + "\\resultado.txt";

            StreamWriter arq = new StreamWriter(path, true);

            arq.WriteLine(conteudo);
            arq.Close();
        }

        private void CriaPasta(string caminhoPasta)
        {
            caminhoPasta = caminhoPasta.Substring(0, caminhoPasta.LastIndexOf("\\"));

            if (!(Directory.Exists(caminhoPasta)))
                Directory.CreateDirectory(caminhoPasta);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ExportarFiles();
        }

        private void ExportarFiles()
        {
            string patch = CaminhoPadrao("", Utils.Enumeracao.TipoDiretorio.LerSaida);
            if (patch != "")
                SalvarComo.SelectedPath = patch;

            //Exibe a caixa de diálogo
            if (SalvarComo.ShowDialog() == DialogResult.OK)
            {
                CaminhoPadrao(SalvarComo.SelectedPath, Utils.Enumeracao.TipoDiretorio.GravarSaida);


                string caminho = SalvarComo.SelectedPath;
                bool erro = false;
                bool passei = false;

                if (caminho != "")
                {
                    string path = caminho + "\\resultado.txt";
                    if (File.Exists(path))
                        File.Delete(path);

                    foreach (DataGridViewRow dg in dataGridView1.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dg.Cells[0];
                        bool checado = Convert.ToBoolean(chk.Value);
                        try
                        {
                            string origem = dg.Cells[2].Value.ToString() + "\\" + dg.Cells[1].Value.ToString();
                            if (checado)
                            {
                                passei = true;
                                if (checkBox2.Checked)
                                {
                                    string destino = origem.Replace(ckbcaminho.SelectedValue.ToString(), caminho);
                                    CriaPasta(destino);
                                    File.Copy(origem, destino, true);
                                }
                                if (ckbresultado.Checked)
                                    CriaArquivo(origem, caminho);

                                toolStripStatusLabel1.Text = origem + " gravado";
                                Application.DoEvents();
                            }
                        }
                        catch (Exception ex)
                        {
                            erro = true;
                            MessageBox.Show(ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    if (erro == false && passei)
                        MessageBox.Show("Arquivos gerados com sucesso", "Parabéns", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    else
                        MessageBox.Show("Você precisa selecionar ao menos um arquivo", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void ckbTudo_CheckedChanged(object sender, EventArgs e)
        {
            PreencheTabelas();
        }

        private void PreencheTabelas()
        {           
        }   

        private void button4_Click(object sender, EventArgs e)
        {
            Cancelar();
        }

        private static void Cancelar()
        {
            if (MessageBox.Show("Você ira perder todo processamento realizado até o momento, você confirma?", "Atenção", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                Application.Restart();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LimparListas();
        }

        private void LimparListas()
        {
            listResultado.Items.Clear();
            dataGridView1.DataSource = null;
            ckbcaminho.SelectedValue = "";
            button1.Enabled = false;
            button5.Enabled = false;
            groupBox2.Enabled = false;
            groupBox4.Enabled = false;
            groupBox1.Enabled = false;
            dateTimePicker1.Value = DateTime.Today.AddDays(-20);
            toolStripStatusLabel1.Text = "";
        }

        private void localizarDiretórioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Localizar();
        }

        private void cancelarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cancelar();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("Sair do aplicativo?", "Sair", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
            Application.Exit();
        }
              
        private void MenuListar_Click(object sender, EventArgs e)
        {
            ListarArquivos();
        }

        private void MenuLimpar_Click(object sender, EventArgs e)
        {
            LimparListas();
        }

        private void MenuExportar_Click(object sender, EventArgs e)
        {
            ExportarFiles();
        }

        private void ckbcaminho_SelectedIndexChanged(object sender, EventArgs e)
        {
            button2.Enabled = ckbcaminho.SelectedIndex == 0;
            button1.Enabled = !button2.Enabled;
        }
    }
}
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
        public enum TipoDiretorio
        {
            GravarEntrada = 1,
            LerEntrada = 2,
            GravarSaida = 3,
            LerSaida = 4
        }


        public Main()
        {
            InitializeComponent();
        }
        private string pastaAplicacao = Application.StartupPath;
        //protected void Desativa(string msg)
        //{
        //    ckbtabelas.Items.Clear();
        //    groupBox1.Enabled = false;
        //    groupBox2.Enabled = false;
        //    btnGerar.Enabled = false;
        //    toolStripStatusLabel1.Text = msg;
        //}
        //protected void Ativa()
        //{
        //    groupBox1.Enabled = true;
        //    groupBox2.Enabled = true;
        //    btnGerar.Enabled = true;
        //}
        public enum TipoRetorno
        {
            WsDataExterno = 1,
            WsDataInterno = 2,
            Select = 3,
            Insert = 4,
            Update = 5
        }
        public string TrataPalavra(string texto)
        {
            string textor = "";

            for (int i = 0; i < texto.Length; i++)
            {
                if (texto[i].ToString() == "ã") textor += "a";
                else if (texto[i].ToString() == "á") textor += "a";
                else if (texto[i].ToString() == "à") textor += "a";
                else if (texto[i].ToString() == "â") textor += "a";
                else if (texto[i].ToString() == "ä") textor += "a";
                else if (texto[i].ToString() == "é") textor += "e";
                else if (texto[i].ToString() == "è") textor += "e";
                else if (texto[i].ToString() == "ê") textor += "e";
                else if (texto[i].ToString() == "ë") textor += "e";
                else if (texto[i].ToString() == "í") textor += "i";
                else if (texto[i].ToString() == "ì") textor += "i";
                else if (texto[i].ToString() == "ï") textor += "i";
                else if (texto[i].ToString() == "õ") textor += "o";
                else if (texto[i].ToString() == "ó") textor += "o";
                else if (texto[i].ToString() == "ò") textor += "o";
                else if (texto[i].ToString() == "ö") textor += "o";
                else if (texto[i].ToString() == "ú") textor += "u";
                else if (texto[i].ToString() == "ù") textor += "u";
                else if (texto[i].ToString() == "ü") textor += "u";
                else if (texto[i].ToString() == "ç") textor += "c";
                else if (texto[i].ToString() == "Ã") textor += "A";
                else if (texto[i].ToString() == "Á") textor += "A";
                else if (texto[i].ToString() == "À") textor += "A";
                else if (texto[i].ToString() == "Â") textor += "A";
                else if (texto[i].ToString() == "Ä") textor += "A";
                else if (texto[i].ToString() == "É") textor += "E";
                else if (texto[i].ToString() == "È") textor += "E";
                else if (texto[i].ToString() == "Ê") textor += "E";
                else if (texto[i].ToString() == "Ë") textor += "E";
                else if (texto[i].ToString() == "Í") textor += "I";
                else if (texto[i].ToString() == "Ì") textor += "I";
                else if (texto[i].ToString() == "Ï") textor += "I";
                else if (texto[i].ToString() == "Õ") textor += "O";
                else if (texto[i].ToString() == "Ó") textor += "O";
                else if (texto[i].ToString() == "Ò") textor += "O";
                else if (texto[i].ToString() == "Ö") textor += "O";
                else if (texto[i].ToString() == "Ú") textor += "U";
                else if (texto[i].ToString() == "Ù") textor += "U";
                else if (texto[i].ToString() == "Ü") textor += "U";
                else if (texto[i].ToString() == "Ç") textor += "C";
                else if (texto[i].ToString() == "/") textor += "_";
                else if (texto[i].ToString() == " ") textor += "_";
                else if (texto[i].ToString() == "%") textor += "_";
                else if (texto[i].ToString() == "") textor += "";
                else if (texto[i].ToString() == "?") textor += "";
                else if (texto[i].ToString() == "(") textor += "";
                else if (texto[i].ToString() == ")") textor += "";
                else textor += texto[i];
            }
            return textor;
        }

        #region Manipulação do registro do windows
        private string CaminhoPadrao(string caminho, TipoDiretorio tipo)
        {
            string retorno = "";
            string key = "ENTRADA";

            if (tipo == TipoDiretorio.GravarSaida || tipo == TipoDiretorio.LerSaida)
                key = "SAIDA";

            try
            {
                if (tipo == TipoDiretorio.GravarEntrada || tipo == TipoDiretorio.GravarSaida)
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

        private void GeraWS(string tabela, string caminho, int numtabelas)
        {
            ////Página Modelo
            //string pathOrigem = Application.StartupPath + "\\MODELO.prw";
            //StreamReader leitura = new StreamReader(pathOrigem, System.Text.Encoding.GetEncoding("ISO-8859-1"));
            //string texto = leitura.ReadToEnd();
            //string NomeWS = "";
            //if (txtalias.Text == "")
            //    NomeWS = "RET" + tabela;
            //else
            //{
            //    if (numtabelas == 1)
            //        NomeWS = txtalias.Text;
            //    else
            //        NomeWS = txtalias.Text + "_" + tabela; // #NOMEWS#
            //}                

            //string WSDATA = GetDados(tabela,TipoRetorno.WsDataExterno,NomeWS); // #WSDATA#
            //string SELECT = GetDados(tabela, TipoRetorno.Select,NomeWS); // #SELECT#
            //string WSINTERNO = GetDados(tabela, TipoRetorno.WsDataInterno, NomeWS);
            //string WSINSERT = GetDados(tabela, TipoRetorno.Insert, NomeWS);
            //string WSUPDATE = GetDados(tabela, TipoRetorno.Update, NomeWS);

            //int index = WSDATA.IndexOf("#");
            //int index1 = WSINSERT.IndexOf("#");
            //int index2 = WSINSERT.IndexOf(",");
            //string ChavePrimaria = TrataPalavra(WSDATA.Substring(0, index));
            //string Campoprimario = TrataPalavra(TrataPalavra(WSINSERT.Substring(index2 + 1, index1 - index2 - 1)));
            //WSDATA = WSDATA.Substring(index + 1);
            //SELECT = "\"" + SELECT.Substring(index + 1);
            //WSINTERNO = WSINTERNO.Substring(index + 1);
            //WSINSERT = WSINSERT.Substring(index1 + 1);
            //WSUPDATE = WSUPDATE.Substring(index + 1);

            ////trocando as variaveis
            //texto = texto.Replace("#NOMEWS#", NomeWS);
            //texto = texto.Replace("#WSDATA#", WSDATA);
            //texto = texto.Replace("#SELECT#", SELECT);
            //texto = texto.Replace("#TABELA#", tabela);
            //texto = texto.Replace("#CHAVE#", ChavePrimaria);
            //texto = texto.Replace("#WSINTERNO#", WSINTERNO);
            //texto = texto.Replace("#INSERTDADOS#", WSINSERT);
            //texto = texto.Replace("#UPDATEDADOS#", WSUPDATE);
            //texto = texto.Replace("#CAMPOPRIMARIO#", Campoprimario);
            //CriaArquivo(texto, "WS" + tabela + ".prw", caminho);
        }


        protected void CarregaArquivo(string ender)
        {
            //OleDbConnection conn;
            //OleDbCommand cmd;
            //OleDbDataReader dr;
            //int ini = ender.LastIndexOf("sx3");
            //string banco = "";
            //if (ini <= 0)
            //{
            //    MessageBox.Show("O arquivo selecionado não é valido,por favor, tente novamente", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    Desativa("Erro no carregamento do arquivo!");
            //    return;
            //}
            //else
            //{
            //    banco = ender.Substring(ini, (ender.Length - ini));
            //    ender = ender.Substring(0, ini - 1);
            //}

            //conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ender + ";Extended Properties=dBase III");
            //try
            //{
            //    toolStripStatusLabel1.Text = "Abrindo conexão com o arquivo " + banco + " ...";
            //    Application.DoEvents();
            //    conn.Open();
            //    string strquery = "select DISTINCT(X3_ARQUIVO) from " + banco + " ORDER BY X3_ARQUIVO";
            //    if (txtfamilia.Text != "")
            //        strquery = "select DISTINCT(X3_ARQUIVO) from " + banco + " where X3_ARQUIVO LIKE '" + txtfamilia.Text + "%' ORDER BY X3_ARQUIVO";

            //    cmd = new OleDbCommand(strquery, conn);
            //    dr = cmd.ExecuteReader();
            //    ckbtabelas.Items.Clear();
            //    toolStripStatusLabel1.Text = "Selecionando Registros...";
            //    Application.DoEvents();
            //    int conta = 0;
            //    while (dr.Read())
            //    {
            //        conta = 1;
            //        ckbtabelas.Items.Add(dr["X3_ARQUIVO"].ToString(), checkBox1.Checked);
            //        toolStripStatusLabel1.Text = "Aguarde carregando " + dr["X3_ARQUIVO"].ToString() + "...";
            //        Application.DoEvents();
            //    }
            //    if (conta == 0)
            //    {
            //        MessageBox.Show("A familia " + txtfamilia.Text + " não pode ser localizada", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        toolStripStatusLabel1.Text = "Familia não localizada!";
            //    }
            //    else
            //    {
            //        toolStripStatusLabel1.Text = "Arquivo carregado com sucesso!";
            //        Ativa();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    Desativa("Erro no carregamento do arquivo!");
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string nome = txtcaminho.Text;
            //button1.Enabled = false;
            //button1.Text = "Aguarde carregando arquivo...";
            //CarregaArquivo(nome);
            //button1.Enabled = true;
            //button1.Text = "Abrir Arquivo SX3";
        }

        protected void CarregaCombo()
        {
            ckbcaminho.DropDownStyle = ComboBoxStyle.DropDownList;
            ckbcaminho.DataSource = INI.GetArquivo();
            ckbcaminho.DisplayMember = "Nome";
            ckbcaminho.ValueMember = "Endereco";
            //ckbcaminho.Update();

        }

        DataGridViewCheckBoxColumn c1;
        CheckBox ckBox;

        private void Main_Load(object sender, EventArgs e)
        {
            dateTimePicker1.MaxDate = DateTime.Today;
            dateTimePicker1.Value = DateTime.Today.AddDays(-20);
            CarregaCombo();

            //c1 = new DataGridViewCheckBoxColumn();
            //c1.Width = 30;
            //c1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            ////c1.Name = "selection";
            ////c1.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //this.dataGridView1.Columns.Add(c1);

            ckBox = new CheckBox();
            //Get the column header cell bounds
            Rectangle rect = this.dataGridView1.GetCellDisplayRectangle(0, -1, true);
            rect.Y = 4;
            rect.X = rect.Location.X + (rect.Width / 4);
            ckBox.Size = new Size(14, 14);
            //Change the location of the CheckBox to make it stay on the header
            ckBox.Location = rect.Location;
            ckBox.CheckedChanged += new EventHandler(ckBox_CheckedChanged);
            //Add the CheckBox into the DataGridView
            //ckBox.Checked = true;
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
        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            CarregaArquivo(ckbcaminho.SelectedValue.ToString());
        }

        protected bool PodeInserir(int lista)
        {
            return true;
            //if (r8.Checked)
            //    return true;
            //else
            //{
            //    if (r1.Checked)
            //        return lista < Convert.ToInt32(r1.Text);
            //    else if (r2.Checked)
            //        return lista < Convert.ToInt32(r2.Text);
            //    else if (r3.Checked)
            //        return lista < Convert.ToInt32(r3.Text);
            //    else if (r4.Checked)
            //        return lista < Convert.ToInt32(r4.Text);
            //    else if (r5.Checked)
            //        return lista < Convert.ToInt32(r5.Text);
            //    else if (r6.Checked)
            //        return lista < Convert.ToInt32(r6.Text);
            //    else
            //        return lista < Convert.ToInt32(r7.Text);
            //}

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

        protected void DirSearch(string sDir, List<Arquivos> lista)
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
                    bool pode = PodeInserir(listResultado.Items.Count);
                    if (!pode)
                        break;

                    FileInfo[] filePasta = d.GetFiles("*.*");
                    List<FileInfo> infoPasta = filePasta.Where(item => (item.LastWriteTime >= dateTimePicker1.Value) && (Desconsiderar(item.Extension))).ToList();
                    nome = d.FullName;

                    foreach (FileInfo fileinfo in infoPasta.OrderByDescending(a => a.LastWriteTime))
                    {


                        FileStream fs = new FileStream(fileinfo.DirectoryName + "\\" + fileinfo.Name, FileMode.Open, FileAccess.Read);
                        Encoding r = GetFileEncoding(fs);
                        fs.Close();





                        Arquivos arq = new Arquivos();


                        arq.Data = fileinfo.LastWriteTime;
                        arq.FullName = fileinfo.DirectoryName;
                        arq.Name = fileinfo.Name;
                        //arq.Encoding = sr.CurrentEncoding + " - " + sr.CurrentEncoding.BodyName + " - " + sr.CurrentEncoding.HeaderName + " - " + sr.CurrentEncoding.EncodingName;



                        arq.Encoding = r.BodyName;


                        arq.Tamanho = fileinfo.Length;
                        pode = PodeInserir(listResultado.Items.Count);

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
                            break;
                        else
                        {
                            lista.Add(arq);
                            listResultado.Items.Add(arq.FullName + "\\" + arq.Name + "----" + arq.Data);
                        }


                        toolStripStatusLabel1.Text = fileinfo.Name;
                        Application.DoEvents();

                    }
                    DirSearch(nome, lista);
                }

            }
            catch (System.Exception excpt)
            {
                Arquivos arq = new Arquivos();



                arq.Data = DateTime.Now;
                arq.FullName = "";
                arq.Name = excpt.Message;
                arq.Encoding = "Não tem";
                arq.OK = false;
                arq.Tamanho = 0;
                lista.Add(arq);

                //StreamReader sr = new StreamReader(arq.FullName + "\\" + arq.Name, true);
                //listResultado.Items.Add(arq.FullName + "\\" + arq.Name + "----" + arq.Data);
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
                //Menu1.Enabled = button1.Enabled;
                //Menu2.Enabled = button4.Enabled;
                //MenuListar.Enabled = button1.Enabled;

                List<Arquivos> lista = new List<Arquivos>();
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
                    Arquivos arq = new Arquivos();

                    if (fileinfo.DirectoryName != "")
                    {
                        //StreamReader sr = new StreamReader(fileinfo.DirectoryName + "\\" + fileinfo.Name, true);

                        //var file = sr.ReadToEnd();



                        //arq.Encoding = "---mara---" + sr.CurrentEncoding + " - " + sr.CurrentEncoding.BodyName + " - " + sr.CurrentEncoding.HeaderName + " - " + sr.CurrentEncoding.EncodingName;
                    }

                    //Encoding xxx;
                    //string xe = fileinfo.DirectoryName + "\\" + fileinfo.Name;
                    //xxx = GetFileEncoding(xe);

                    FileStream fs = new FileStream(fileinfo.DirectoryName + "\\" + fileinfo.Name, FileMode.Open, FileAccess.Read);
                    Encoding r = GetFileEncoding(fs);
                    fs.Close();

                    arq.Encoding = r.BodyName;


                    arq.Data = fileinfo.LastWriteTime;
                    arq.FullName = fileinfo.DirectoryName;
                    arq.Name = fileinfo.Name;

                    arq.Tamanho = fileinfo.Length;


                    bool pode = PodeInserir(listResultado.Items.Count);


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


                    foreach (Arquivos file in lista)
                    {
                        if (file.FullName != "")
                        {
                            //StreamReader sr = new StreamReader(file.FullName + "\\" + file.Name, true);

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
                    //MenuExportar.Enabled = true;
                    toolStripStatusLabel1.Text = lista.Count.ToString() + " Arquivos afetados";
                    Application.DoEvents();

                    dateTimePicker1.Value = vlPadrao;
                }

                button1.Enabled = true;
                button4.Enabled = !button1.Enabled;
                //Menu1.Enabled = button1.Enabled;
                //Menu2.Enabled = button4.Enabled;
                //MenuListar.Enabled = button1.Enabled;
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

            // aguarde f = new aguarde();
            // f.StartPosition = FormStartPosition.CenterParent;
            //  f.Show();
            Processa();
            button1.Text = "Listar arquivos";
            button1.Enabled = true;
            button5.Enabled = true;
            //MenuLimpar.Enabled = true;
            //  f.Close();
        }

        //TODO: FAZER FUNCIONAR A DEPENDENCIA DE CACHE ABAIXO
        //private static TreeNode CreateDirectoryNode(List<Arquivos> lista)
        //{
        //    TreeNode directoryNode = new TreeNode("teste");
        //    foreach (Arquivos arq in lista)
        //    {

        //    }

        //    if (consideraData)
        //    {
        //        foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
        //            directoryNode.Nodes.Add(CreateDirectoryNode(directory, consideraData, data));
        //        foreach (FileInfo file in directoryInfo.GetFiles())
        //            directoryNode.Nodes.Add(new TreeNode(file.Name + " - " + file.LastWriteTime.ToString()));
        //        return directoryNode;
        //    }
        //    else
        //    {
        //        foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
        //            directoryNode.Nodes.Add(CreateDirectoryNode(directory, consideraData, data));

        //        foreach (FileInfo file in directoryInfo.GetFiles())
        //        {
        //            data = Convert.ToDateTime(data.ToString("dd/MM/yyyy"));

        //            if (file.LastWriteTime >= data)
        //            {
        //                directoryNode.Nodes.Add(new TreeNode(file.Name + " - " + file.LastWriteTime.ToString()));
        //            }
        //        }

        //        return directoryNode;
        //    }
        //}


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
            string patch = CaminhoPadrao("", TipoDiretorio.LerSaida);
            if (patch != "")
                SalvarComo.SelectedPath = patch;

            //Exibe a caixa de diálogo
            if (SalvarComo.ShowDialog() == DialogResult.OK)
            {
                CaminhoPadrao(SalvarComo.SelectedPath, TipoDiretorio.GravarSaida);


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
            //bool checado = ckbTudo.Checked;

            //foreach (DataGridViewRow dg in dataGridView1.Rows)
            //{
            //    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dg.Cells[0];
            //    chk.Value = checado;
            //}

            ////for (int i = 0; i < listResultado.Items.Count; i++)
            ////    listResultado.SetItemChecked(i, checado);
        }


        //public static Encoding GetFileEncoding(string srcFile)
        public static Encoding GetFileEncoding(FileStream fs)
        {

            byte[] Unicode = new byte[] { 0xFF, 0xFE, 0x41 };
            byte[] UnicodeBIG = new byte[] { 0xFE, 0xFF, 0x00 };
            byte[] UTF8 = new byte[] { 0xEF, 0xBB, 0xBF }; //with BOM
            //byte[] UTF8 = new byte[] { 0xef, 0xbb, 0xbf, 0x41 }; 
            Encoding reVal = System.Text.Encoding.Default;

            BinaryReader r = new BinaryReader(fs, System.Text.Encoding.Default);
            int i;
            int.TryParse(fs.Length.ToString(), out i);
            byte[] ss = r.ReadBytes(i);

            if (IsUTF8Bytes(ss) || (ss[0] == 0xEF && ss[1] == 0xBB && ss[2] == 0xBF))
            {
                if (ss.Length >= 3 && ss[0] == 0xEF && ss[1] == 0xBB && ss[2] == 0xBF)
                {
                    reVal = System.Text.Encoding.UTF7;
                }
                else
                {
                    reVal = System.Text.Encoding.UTF8;
                }

            }
            else if (ss[0] == 0xFE && ss[1] == 0xFF && ss[2] == 0x00)
            {
                reVal = System.Text.Encoding.BigEndianUnicode;
            }
            else if (ss[0] == 0xFF && ss[1] == 0xFE && ss[2] == 0x41)
            {
                reVal = System.Text.Encoding.Unicode;
            }
            r.Close();
            return reVal;


            //// *** Use Default of Encoding.Default (Ansi CodePage)
            //Encoding enc = System.Text.Encoding.Default;

            //// *** Detect byte order mark if any - otherwise assume default
            //byte[] buffer = new byte[5];
            //FileStream file = new FileStream(srcFile, FileMode.Open);
            //file.Read(buffer, 0, 5);
            //file.Close();

            //if (buffer[0] == 0xef && buffer[1] == 0xbb && buffer[2] == 0xbf)
            //    enc = System.Text.Encoding.UTF8;
            //else if (buffer[0] == 0xfe && buffer[1] == 0xff)
            //    enc = System.Text.Encoding.Unicode;
            //else if (buffer[0] == 0 && buffer[1] == 0 && buffer[2] == 0xfe && buffer[3] == 0xff)
            //    enc = System.Text.Encoding.UTF32;
            //else if (buffer[0] == 0x2b && buffer[1] == 0x2f && buffer[2] == 0x76)
            //    enc = System.Text.Encoding.UTF7;
            //else if (buffer[0] == 0xFE && buffer[1] == 0xFF)
            //    // 1201 unicodeFFFE Unicode (Big-Endian)
            //    enc = System.Text.Encoding.GetEncoding(1201);
            //else if (buffer[0] == 0xFF && buffer[1] == 0xFE)
            //    // 1200 utf-16 Unicode
            //    enc = System.Text.Encoding.GetEncoding(1200);


            //return enc;
        }

        private static bool IsUTF8Bytes(byte[] data)
        {
            int charByteCounter = 1;
            byte curByte;
            for (int i = 0; i < data.Length; i++)
            {
                curByte = data[i];
                if (charByteCounter == 1)
                {
                    if (curByte >= 0x80)
                    {
                        while (((curByte <<= 1) & 0x80) != 0)
                        {
                            charByteCounter++;
                        }

                        if (charByteCounter == 1 || charByteCounter > 6)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    if ((curByte & 0xC0) != 0x80)
                    {
                        return false;
                    }
                    charByteCounter--;
                }
            }
            if (charByteCounter > 1)
            {
                throw new Exception("Error byte format");
            }
            return true;
        }



        //static Encoding getEncoding(string path)
        //{
        //    var stream = new FileStream(path, FileMode.Open);
        //    var reader = new StreamReader(stream, Encoding.Default, true);
        //    reader.Read();

        //    if (reader.CurrentEncoding != Encoding.Default)
        //    {
        //        reader.Close();
        //        return reader.CurrentEncoding;
        //    }

        //    stream.Position = 0;

        //    reader = new StreamReader(stream, new UTF8Encoding(false, true));
        //    try
        //    {
        //        reader.ReadToEnd();
        //        reader.Close();
        //        return Encoding.UTF8;
        //    }
        //    catch (Exception)
        //    {
        //        reader.Close();
        //        return Encoding.Default;
        //    }
        //}



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
            //Application.Restart();
            listResultado.Items.Clear();
            dataGridView1.DataSource = null;
            ckbcaminho.SelectedValue = "";
            button1.Enabled = false;
            button5.Enabled = false;
            //MenuListar.Enabled = false;
            //Menu2.Enabled = false;
            //MenuExportar.Enabled = false;
            //MenuLimpar.Enabled = false;
            groupBox2.Enabled = false;
            groupBox4.Enabled = false;
            groupBox1.Enabled = false;
            dateTimePicker1.Value = DateTime.Today.AddDays(-20);
            toolStripStatusLabel1.Text = "";
        }

        private void txtcaminho_TextChanged(object sender, EventArgs e)
        {

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
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

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
            //button2.Enabled = ckbcaminho.SelectedValue.ToString() == "";
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TibbiIstatistikProje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Visible = false;
  
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            linkLabel1.Visible = false;
            linkLabel2.Visible = false;
            linkLabel3.Visible = false;
            linkLabel4.Visible = false;
            linkLabel5.Visible = false;
            linkLabel6.Visible = false;
            linkLabel7.Visible = false;
            linkLabel8.Visible = false;
            linkLabel9.Visible = false;
            linkLabel10.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            button7.Visible = false;

        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog Dosya = new OpenFileDialog();
            Dosya.ShowDialog();
            string dosyayolu = Dosya.FileName;
            OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + dosyayolu + "; Extended Properties=Excel 12.0");
            baglanti.Open();
            DataTable dbSchema = baglanti.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            if (dbSchema == null || dbSchema.Rows.Count < 1)
            {
                throw new Exception("Error: Could not determine the name of the first worksheet.");
            }
            string IlkSayfaIsmi = dbSchema.Rows[0]["TABLE_NAME"].ToString();

            string komut = "Select* from[" + IlkSayfaIsmi + "]";
            OleDbCommand cmd = new OleDbCommand(komut, baglanti);
            OleDbDataAdapter da = new OleDbDataAdapter(komut, baglanti);
            OleDbDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt.DefaultView;
            baglanti.Close();

        }
        public class TwoingAlgoritmasi
        {
            public double[,] AnaVeri;            
            private double[] Yas;
            private double[] Cinsiyet;
            private double[] GAT;//Göğüs Ağrısı Tipi
            private double[] KanBasinci;
            private double[] SerumKolestrol;
            private double[] KanSekeri;//>120
            private double[] Elektrokardiyografi;
            private double[] MaksimumKalpHizi;
            private double[] ABE;//Anjine bağlı egzersiz
            private double[] STDepresyonu;
            private double[] STegimi;
            private double[] BuyukDamarlar;
            private double[] Talasemi;
            private double[] Sinif;
            private double[] sonuc=new double[25];
            public double buyuk,Sonucindex; 
            public int[] SagSolKayitSayi;

            public void IlkDeger(int satir)
            {
            Yas = new double[satir];
            Cinsiyet = new double[satir];
            GAT = new double[satir];//Göğüs Ağrısı Tipi
            KanBasinci = new double[satir];
            SerumKolestrol = new double[satir];
            KanSekeri = new double[satir];//>120
            Elektrokardiyografi = new double[satir];
            MaksimumKalpHizi = new double[satir];
            ABE = new double[satir];//Anjine bağlı egzersiz
            STDepresyonu = new double[satir];
            STegimi = new double[satir];
            BuyukDamarlar = new double[satir];
            Talasemi = new double[satir];
            Sinif = new double[satir];
        }
            public double İkiyeBolHesapla(int a,int satir,double[] Dizi)
            {
                int  PsolKayit = 0, PsagKayit = 0;
                double Tsinif1sol = 0, Tsinif1sag = 0, Tsinif2sol = 0, Tsinif2sag = 0, Psol, Psag;
                double Soldaki1 = 0, Soldaki2 = 0, Sagdaki1 = 0, Sagdaki2 = 0;
                double sonuc;
                for (int k = 0; k < satir; k++)
                    if (Dizi[k]==a)
                    {
                        PsolKayit++;
                        if (Sinif[k] == 1)
                            Soldaki1++;
                        else if (Sinif[k] == 2)
                            Soldaki2++;

                    }
                    else
                    {
                        PsagKayit++;
                        if (Sinif[k] == 1)
                            Sagdaki1++;
                        else if (Sinif[k] == 2)
                            Sagdaki2++;

                    }
                Psol = (double)PsolKayit /satir;
                Psag = (double)PsagKayit /satir;
                Tsinif1sol = (Soldaki1 / PsolKayit);
                Tsinif2sol = (Soldaki2 / PsolKayit);
                Tsinif1sag = (Sagdaki1 / PsagKayit);
                Tsinif2sag = (Sagdaki2 / PsagKayit);

                sonuc = 2 * Psol * Psag * (Math.Abs(Tsinif1sol - Tsinif1sag) + Math.Abs(Tsinif2sol - Tsinif2sag));
                return sonuc;
            }
            public double OrtalamaİleİkiyeBol( int satir, double[] Dizi)
            {
                double sonuc;
                int toplam = 0, PsolKayit = 0, PsagKayit = 0;
                double Tsinif1sol = 0, Tsinif1sag = 0, Tsinif2sol = 0, Tsinif2sag = 0, Psol, Psag,ortalama;
                double Soldaki1 = 0, Soldaki2 = 0, Sagdaki1 = 0, Sagdaki2 = 0;
                for (int j = 0; j < satir; j++)
                    toplam += (int)Dizi[j];
                ortalama = Math.Round((double)toplam / satir);
                for (int k = 0; k < satir; k++)
                    if (Dizi[k] <= ortalama)
                    {
                        PsolKayit++;
                        if (Sinif[k] == 1)
                            Soldaki1++;
                        else if (Sinif[k] == 2)
                            Soldaki2++;

                    }
                    else
                    {
                        PsagKayit++;
                        if (Sinif[k] == 1)
                            Sagdaki1++;
                        else if (Sinif[k] == 2)
                            Sagdaki2++;

                    }
                Psol = (double)PsolKayit / 135;
                Psag = (double)PsagKayit / 135;
                Tsinif1sol = (Soldaki1 / PsolKayit);
                Tsinif2sol = (Soldaki2 / PsolKayit);
                Tsinif1sag = (Sagdaki1 / PsagKayit);
                Tsinif2sag = (Sagdaki2 / PsagKayit);

                sonuc = 2 * Psol * Psag * (Math.Abs(Tsinif1sol - Tsinif1sag) + Math.Abs(Tsinif2sol - Tsinif2sag));

                return sonuc;
            }
            public double[,] YeniKayitIkiyeBolmeIle(int a, int satir, double[] Dizi)
            {

                int SagKayit = 0;
                int[] SagSolkayit=new int[2];
                SagSolkayit[0] = 0;//Sınıf 1 olanlar
                SagSolkayit[1] = 0;//Sınıf 2 olanlar
                for (int i= 0; i < satir; i++)
                    if (Dizi[i] == a)
                    {
                        if (Dizi[i] == a)
                        {
                            if (Sinif[i] == 1)
                                SagSolkayit[0]++;
                            else if (Sinif[i] == 2)
                                SagSolkayit[1]++;

                        }
                        
                    }
                    else
                    {
                        SagKayit++;


                    }
                SagSolKayitSayi = SagSolkayit;
                double[,] deneme = new double[SagKayit, 14];
                int k =0 ;
                for (int i = 0; i < satir; i++)
                   
                        if (Dizi[i] != a)
                        {
                            for (int j = 0; j < 14; j++)
                            deneme[k,j] = AnaVeri[i, j];
                            if (k != satir)
                                k++;
                          
                           
                           

                        }
                return deneme;
            }
            public double[,] YeniKayitOrtalamaIle(int satir, double[] Dizi)
            {
                
                int toplam = 0, SagKayit = 0;
                double  ortalama;
                for (int j = 0; j < satir; j++)
                    toplam += (int)Dizi[j];
                ortalama = (double)toplam / satir;
                int[] SagSolkayit = new int[2];
                SagSolkayit[0] = 0;//Sınıf 1 olanlar
                SagSolkayit[1] = 0;//Sınıf 2 olanlar
                for (int i = 0; i < satir; i++)
                    if (Dizi[i] == ortalama)
                    {
                        if (Dizi[i] == ortalama)
                        {
                            if (Sinif[i] == 1)
                                SagSolkayit[0]++;
                            else if (Sinif[i] == 2)
                                SagSolkayit[1]++;

                        }

                    }
                    else
                    {
                        SagKayit++;


                    }
                SagSolKayitSayi = SagSolkayit;
                double[,] deneme = new double[SagKayit, 14];
                int k = 0;
                for (int i = 0; i < satir; i++)

                    if (Dizi[i] > ortalama)
                    {
                        for (int j = 0; j < 14; j++)
                            deneme[k, j] = AnaVeri[i, j];
                        if (k !=satir)
                            k++;




                    }
                return deneme;
            }
            

            public void DiziyeAktar(double[,] Veri)
            {
           
                for (int i = 0; i <Veri.GetUpperBound(1)+1; i++)
                    for (int j = 0; j < Veri.GetUpperBound(0)+1; j++)
                    {
                        switch (i)
                        {
                            case 0:
                                try
                                {
                                    Yas[j] = Veri[j,i];
                                }
                                catch (Exception e)
                                {
                                    MessageBox.Show("hata var="+e);
                                    throw;
                                }
                                
                                break;
                            case 1:
                                Cinsiyet[j] = Veri[j,i];
                                break;
                            case 2:
                                GAT[j] = Veri[j,i];
                                break;
                            case 3:
                                KanBasinci[j] = Veri[j,i];
                                break;
                            case 4:
                                SerumKolestrol[j] = Veri[j,i];
                                break;
                            case 5:
                                KanSekeri[j] = Veri[j,i];
                                break;
                            case 6:
                                Elektrokardiyografi[j] =Veri[j,i];
                                break;
                            case 7:
                                MaksimumKalpHizi[j] = Veri[j,i];
                                break;
                            case 8:
                                ABE[j] = Veri[j,i];
                                break;
                            case 9:
                                STDepresyonu[j] = Veri[j,i];
                                break;
                            case 10:
                                STegimi[j] = Veri[j,i];
                                break;
                            case 11:
                                BuyukDamarlar[j] = Veri[j,i];
                                break;
                            case 12:
                                Talasemi[j] = Veri[j,i];
                                break;
                            case 13:
                                Sinif[j] = Veri[j,i];
                                break;
                        }


                    }
            }
            public void AdayBolunme(int satir)
            {
               
                

                for (int i=0;i<25;i++)
                {
                   
                    switch (i)
                    {
                        case 0:
                            sonuc[i] = OrtalamaİleİkiyeBol(satir, Yas);

                            break;
                        case 1:
                            sonuc[i] = İkiyeBolHesapla(1, satir, Cinsiyet);
                            break;
                        case 2:
                            sonuc[i] = İkiyeBolHesapla(1, satir, GAT);
                            break;
                        case 3:
                            sonuc[i] = İkiyeBolHesapla(2, satir, GAT);
                            break;
                        case 4:
                            sonuc[i] = İkiyeBolHesapla(3, satir, GAT);
                            break;
                        case 5:
                            sonuc[i] = İkiyeBolHesapla(4, satir, GAT);
                            break;
                        
                        case 6:
                            sonuc[i] = OrtalamaİleİkiyeBol(satir, KanBasinci);
                            break;
                        case 7:
                            sonuc[i] = OrtalamaİleİkiyeBol(satir,SerumKolestrol);
                            break;
                        case 8:
                            sonuc[i] = İkiyeBolHesapla(0, satir, KanSekeri);
                            break;
                        case 9:
                            sonuc[i] = İkiyeBolHesapla(0, satir, Elektrokardiyografi);

                            break;
                        case 10:
                            sonuc[i] = İkiyeBolHesapla(1, satir, Elektrokardiyografi);
                            break;
                        case 11:
                            sonuc[i] = İkiyeBolHesapla(2, satir, Elektrokardiyografi);
                            break;

                        case 12:
                            sonuc[i] = OrtalamaİleİkiyeBol(satir,MaksimumKalpHizi);
                            break;
                        case 13:
                            sonuc[i] = İkiyeBolHesapla(0, satir, ABE);
                            break;
                        case 14:
                            sonuc[i] = OrtalamaİleİkiyeBol(satir, STDepresyonu);
                            break;
                        case 15:
                            sonuc[i] = İkiyeBolHesapla(1, satir, STegimi);
                            break;
                        case 16:
                            sonuc[i] = İkiyeBolHesapla(2, satir, STegimi);
                            break;
                        case 17:
                            sonuc[i] = İkiyeBolHesapla(3, satir, STegimi);
                            break;
                        case 18:
                            sonuc[i] = İkiyeBolHesapla(0, satir, BuyukDamarlar);
                            break;
                        case 19:
                            sonuc[i] = İkiyeBolHesapla(1, satir, BuyukDamarlar);
                            break;
                        case 20:
                            sonuc[i] = İkiyeBolHesapla(2, satir, BuyukDamarlar);
                            break;
                        case 21:
                            sonuc[i] = İkiyeBolHesapla(3, satir, BuyukDamarlar);
                            break;
                        case 22:
                            sonuc[i] = İkiyeBolHesapla(3, satir, Talasemi);
                            break;
                        case 23:
                            sonuc[i] = İkiyeBolHesapla(6, satir, Talasemi);
                            break;
                        case 24:
                            sonuc[i] = İkiyeBolHesapla(7, satir, Talasemi);
                            break;
                       

                    }
                }

            }
            public void EnBuyukSonuc ()
            {
                double enbuyuk =0,index=0;
                for (int i=0;i<sonuc.Length;i++)
                {
                    if (enbuyuk < sonuc[i])
                    {
                        enbuyuk = sonuc[i];
                        index = i;

                    } 
                    
                }

                buyuk = enbuyuk;
                Sonucindex = index;                            
            }
            public void AdayBolunme2(int satir,int i)
            {
                switch (i)
                {
                    case 0:
                        AnaVeri = YeniKayitOrtalamaIle(satir, Yas);

                        break;
                    case 1:
                        AnaVeri = YeniKayitIkiyeBolmeIle(1, satir, Cinsiyet);
                        break;
                    case 2:
                        AnaVeri = YeniKayitIkiyeBolmeIle(1, satir, GAT);
                        break;
                    case 3:
                        AnaVeri = YeniKayitIkiyeBolmeIle(2, satir, GAT);
                        break;
                    case 4:
                        AnaVeri = YeniKayitIkiyeBolmeIle(3, satir, GAT);
                        break;
                    case 5:
                       AnaVeri = YeniKayitIkiyeBolmeIle(4, satir, GAT);
                        break;

                    case 6:
                        AnaVeri = YeniKayitOrtalamaIle(satir, KanBasinci);
                        break;
                    case 7:
                        AnaVeri = YeniKayitOrtalamaIle(satir, SerumKolestrol);
                        break;
                    case 8:
                        AnaVeri = YeniKayitIkiyeBolmeIle(0, satir, KanSekeri);
                        break;
                    case 9:
                        AnaVeri = YeniKayitIkiyeBolmeIle(0, satir, Elektrokardiyografi);

                        break;
                    case 10:
                        AnaVeri = YeniKayitIkiyeBolmeIle(1, satir, Elektrokardiyografi);
                        break;
                    case 11:
                        AnaVeri = YeniKayitIkiyeBolmeIle(2, satir, Elektrokardiyografi);
                        break;

                    case 12:
                        AnaVeri = YeniKayitOrtalamaIle(satir, MaksimumKalpHizi);
                        break;
                    case 13:
                        AnaVeri = YeniKayitIkiyeBolmeIle(0, satir, ABE);
                        break;
                    case 14:
                        AnaVeri = YeniKayitOrtalamaIle(satir, STDepresyonu);
                        break;
                    case 15:
                        AnaVeri = YeniKayitIkiyeBolmeIle(1, satir, STegimi);
                        break;
                    case 16:
                        AnaVeri = YeniKayitIkiyeBolmeIle(2, satir, STegimi);
                        break;
                    case 17:
                        AnaVeri = YeniKayitIkiyeBolmeIle(3, satir, STegimi);
                        break;
                    case 18:
                        AnaVeri = YeniKayitIkiyeBolmeIle(0, satir, BuyukDamarlar);
                        break;
                    case 19:
                        AnaVeri = YeniKayitIkiyeBolmeIle(1, satir, BuyukDamarlar);
                        break;
                    case 20:
                        AnaVeri = YeniKayitIkiyeBolmeIle(2, satir, BuyukDamarlar);
                        break;
                    case 21:
                        AnaVeri = YeniKayitIkiyeBolmeIle(3, satir, BuyukDamarlar);
                        break;
                    case 22:
                        AnaVeri = YeniKayitIkiyeBolmeIle(3, satir, Talasemi);
                        break;
                    case 23:
                        AnaVeri = YeniKayitIkiyeBolmeIle(6, satir, Talasemi);
                        break;
                    case 24:
                        AnaVeri = YeniKayitIkiyeBolmeIle(7, satir, Talasemi);
                        break;


                }
            }
            


            public void islem(DataGridView dataGridView1)
            {
                
                double[,] Veri = new double[dataGridView1.RowCount - 1, dataGridView1.ColumnCount];
                double[,] Veri1 = new double[10, 10];
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    {
                        Veri[i, j] = (double)dataGridView1[j, i].Value;
                    }
                
                AnaVeri = Veri;
                
                IlkDeger(dataGridView1.RowCount - 1);

                for (int i = 0; i < 6; i++)
                {

                    DiziyeAktar(AnaVeri);
                    AdayBolunme(AnaVeri.GetUpperBound(0) + 1);
                    EnBuyukSonuc();
                    AdayBolunme2(AnaVeri.GetUpperBound(0) + 1, (int)Sonucindex);
                    IlkDeger(AnaVeri.GetUpperBound(0) + 1);
                }

            }
           public void YazdirmaIslem(int m, DataGridView dataGridView1)
            {
                double[,] Veri = new double[dataGridView1.RowCount - 1, dataGridView1.ColumnCount];
                
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    {
                        Veri[i, j] = (double)dataGridView1[j, i].Value;
                    }

                AnaVeri = Veri;

                IlkDeger(dataGridView1.RowCount - 1);

                for (int i = 0; i < m; i++)
                {

                    DiziyeAktar(AnaVeri);
                    AdayBolunme(AnaVeri.GetUpperBound(0) + 1);
                    EnBuyukSonuc();
                    AdayBolunme2(AnaVeri.GetUpperBound(0) + 1, (int)Sonucindex);
                    
                    IlkDeger(AnaVeri.GetUpperBound(0) + 1);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TwoingAlgoritmasi deneme = new TwoingAlgoritmasi();
            deneme.islem(dataGridView1);
            button3.Visible = true;
           

        }

        

       

        

       

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TwoingAlgoritmasi deneme = new TwoingAlgoritmasi();
            deneme.YazdirmaIslem(1, dataGridView1);
            MessageBox.Show("Toplam " + deneme.SagSolKayitSayi[1] + " Adet Kayit bulunmustur");

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TwoingAlgoritmasi deneme = new TwoingAlgoritmasi();
            deneme.YazdirmaIslem(1, dataGridView1);
            MessageBox.Show("Toplam " + deneme.SagSolKayitSayi[0] + " Adet Kayit bulunmustur");

        }
        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TwoingAlgoritmasi deneme = new TwoingAlgoritmasi();
            deneme.YazdirmaIslem(2, dataGridView1);
            MessageBox.Show("Toplam " + deneme.SagSolKayitSayi[0] + " Adet Kayit bulunmustur");
        }
        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            TwoingAlgoritmasi deneme = new TwoingAlgoritmasi();
            deneme.YazdirmaIslem(2, dataGridView1);
            MessageBox.Show("Toplam " + deneme.SagSolKayitSayi[1] + " Adet Kayit bulunmustur");
        }

        
        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TwoingAlgoritmasi deneme = new TwoingAlgoritmasi();
            deneme.YazdirmaIslem(3, dataGridView1);
            MessageBox.Show("Toplam " + deneme.SagSolKayitSayi[0] + " Adet Kayit bulunmustur");
        }
        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TwoingAlgoritmasi deneme = new TwoingAlgoritmasi();
            deneme.YazdirmaIslem(3, dataGridView1);
            MessageBox.Show("Toplam " + deneme.SagSolKayitSayi[1] + " Adet Kayit bulunmustur");
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TwoingAlgoritmasi deneme = new TwoingAlgoritmasi();
            deneme.YazdirmaIslem(4, dataGridView1);
            MessageBox.Show("Toplam " + deneme.SagSolKayitSayi[0] + " Adet Kayit bulunmustur");
        }

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TwoingAlgoritmasi deneme = new TwoingAlgoritmasi();
            deneme.YazdirmaIslem(4, dataGridView1);
            MessageBox.Show("Toplam " + deneme.SagSolKayitSayi[1] + " Adet Kayit bulunmustur");
        }

        private void linkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TwoingAlgoritmasi deneme = new TwoingAlgoritmasi();
            deneme.YazdirmaIslem(5, dataGridView1);
            MessageBox.Show("Toplam " + deneme.SagSolKayitSayi[0] + " Adet Kayit bulunmustur");
        }

        private void linkLabel10_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TwoingAlgoritmasi deneme = new TwoingAlgoritmasi();
            deneme.YazdirmaIslem(5, dataGridView1);
            MessageBox.Show("Toplam " + deneme.SagSolKayitSayi[1] + " Adet Kayit bulunmustur");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
            linkLabel1.Visible = true;
            linkLabel2.Visible = true;
            button4.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            label3.Visible = true;
            linkLabel3.Visible = true;
            linkLabel4.Visible = true;
            button5.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {

            label4.Visible = true;
            linkLabel5.Visible = true;
            linkLabel6.Visible = true;
            button6.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            label5.Visible = true;
            linkLabel7.Visible = true;
            linkLabel8.Visible = true;
            button7.Visible = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
            label6.Visible = true;
            linkLabel9.Visible = true;
            linkLabel10.Visible = true;
            
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void linkLabel6_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkLabel5_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            label1.Visible = true;
            linkLabel1.Visible = true;
            linkLabel2.Visible = true;
            button4.Visible = true;

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click_1(object sender, EventArgs e)
        {

        }

        private void linkLabel3_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TwoingAlgoritmasi deneme = new TwoingAlgoritmasi();
            deneme.YazdirmaIslem(2, dataGridView1);
            MessageBox.Show(deneme.SagSolKayitSayi[0] + "Adet Kayıt Bulundu");
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TwoingAlgoritmasi deneme = new TwoingAlgoritmasi();
            deneme.YazdirmaIslem(1, dataGridView1);
            MessageBox.Show(deneme.SagSolKayitSayi[1] + "Adet Kayıt Bulundu");
        }

        private void linkLabel2_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TwoingAlgoritmasi deneme = new TwoingAlgoritmasi();
            deneme.YazdirmaIslem(1, dataGridView1);
            MessageBox.Show( deneme.SagSolKayitSayi[0]+"Adet Kayıt Bulundu");
        }

        private void linkLabel4_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TwoingAlgoritmasi deneme = new TwoingAlgoritmasi();
            deneme.YazdirmaIslem(2, dataGridView1);
            MessageBox.Show(deneme.SagSolKayitSayi[1] + "Adet Kayıt Bulundu");
        }

        private void linkLabel6_LinkClicked_2(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TwoingAlgoritmasi deneme = new TwoingAlgoritmasi();
            deneme.YazdirmaIslem(3, dataGridView1);
            MessageBox.Show(deneme.SagSolKayitSayi[1] + "Adet Kayıt Bulundu");
        }

        private void linkLabel5_LinkClicked_2(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TwoingAlgoritmasi deneme = new TwoingAlgoritmasi();
            deneme.YazdirmaIslem(3, dataGridView1);
            MessageBox.Show(deneme.SagSolKayitSayi[0] + "Adet Kayıt Bulundu");
        }

        private void linkLabel8_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TwoingAlgoritmasi deneme = new TwoingAlgoritmasi();
            deneme.YazdirmaIslem(4, dataGridView1);
            MessageBox.Show(deneme.SagSolKayitSayi[1] + "Adet Kayıt Bulundu");
        }

        private void linkLabel7_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TwoingAlgoritmasi deneme = new TwoingAlgoritmasi();
            deneme.YazdirmaIslem(4, dataGridView1);
            MessageBox.Show(deneme.SagSolKayitSayi[0] + "Adet Kayıt Bulundu");
        }

        private void linkLabel10_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TwoingAlgoritmasi deneme = new TwoingAlgoritmasi();
            deneme.YazdirmaIslem(5, dataGridView1);
            MessageBox.Show(deneme.SagSolKayitSayi[1] + "Adet Kayıt Bulundu");
        }

        private void linkLabel9_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TwoingAlgoritmasi deneme = new TwoingAlgoritmasi();
            deneme.YazdirmaIslem(5, dataGridView1);
            MessageBox.Show(deneme.SagSolKayitSayi[0] + "Adet Kayıt Bulundu");
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;

namespace Integrador_1
{
    public partial class Form1 : Form
    {
        public Empresa empresa;
        public AutoVista av;
        Regex r;
        public Form1()
        {
            InitializeComponent();
            empresa = new Empresa();
            av = new AutoVista();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.MultiSelect = false;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView3.MultiSelect = false;
            dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView4.MultiSelect = false;
            dataGridView4.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                string dni = Interaction.InputBox("DNI: ");
                if (!Information.IsNumeric(dni)) throw new Exception("El DNI no es numérico !!!");
                if(dni.Length>8) throw new Exception("El DNI posee demaciados números !!!");
                dni= $"{dni.Substring(0,2)}.{dni.Substring(2, 3)}.{dni.Substring(5,3)}";
                string nombre = Interaction.InputBox("Nombre: ");
                string apellido = Interaction.InputBox("Apellido: ");
                Persona p = new Persona(dni, nombre, apellido);
                empresa.AgregarPersona(p);
                Mostrar(dataGridView1,empresa.ListaPersonas());
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }
        private void Mostrar(DataGridView pDGV,Object pO)
        { pDGV.DataSource = null;pDGV.DataSource = pO; }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                if (dataGridView1.Rows.Count == 0) throw new Exception("No hay Personas para borrar !!!");
                // dataGridView1.SelectedRows[0].DataBoundItem
                empresa.BorrarPersona(dataGridView1.CurrentRow.DataBoundItem as Persona);
                Mostrar(dataGridView1, empresa.ListaPersonas());
                Mostrar(dataGridView4, av.RetornaAutoVista(empresa.ListaAutos()));
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                if (dataGridView1.Rows.Count == 0) throw new Exception("No hay Personas para modificar !!!");
                Persona personaaux = dataGridView1.CurrentRow.DataBoundItem as Persona;
                personaaux.Nombre = Interaction.InputBox("Nombre: ","Modificando...", personaaux.Nombre);
                personaaux.Apellido = Interaction.InputBox("Apellido: ","Modificando...", personaaux.Apellido);
                empresa.ModificarPersona(personaaux);
                Mostrar(dataGridView1, empresa.ListaPersonas());
                Mostrar(dataGridView4, av.RetornaAutoVista(empresa.ListaAutos()));

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void button6_Click(object sender, EventArgs e)
        {

            try
            {
                string patente = Interaction.InputBox("Patente: ");
                r = new Regex(@"[A-Z]([A-Z][\d]{3}[A-Z]{2}|[\d]{3}[A-Z]{3})");
                if (!r.IsMatch(patente)) throw new Exception("La patente es incorrecta !!!");
                string marca = Interaction.InputBox("Marca: ");
                string modelo = Interaction.InputBox("Modelo: ");
                string axo = Interaction.InputBox("Año: ");
                r = new Regex(@"[\d]{4}");
                if(!r.IsMatch(axo) || axo.Length!=4) throw new Exception("El año debe ser numérico y de 4 dígitos !!!");
                string precio = Interaction.InputBox("Precio: ");
                if (!Information.IsNumeric(precio)) throw new Exception("El precio debe ser numérico !!!");
                Auto a = new Auto(patente,marca,modelo,axo,decimal.Parse(precio));
                empresa.AgregarAuto(a);
                Mostrar(dataGridView2, empresa.ListaAutos());
                //Mostrar(dataGridView4, av.RetornaAutoVista(empresa.ListaAutos()));
                Mostrar(dataGridView4, empresa.RetornaVistaAuto());

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.Rows.Count == 0) throw new Exception("No hay autos para borrar !!!");
                // dataGridView1.SelectedRows[0].DataBoundItem
                empresa.BorrarAuto(dataGridView2.CurrentRow.DataBoundItem as Auto);
                Mostrar(dataGridView2, empresa.ListaAutos());
                Mostrar(dataGridView4, av.RetornaAutoVista(empresa.ListaAutos()));
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.Rows.Count == 0) throw new Exception("No hay Autos para modificar !!!");
                Auto autoaux = dataGridView2.CurrentRow.DataBoundItem as Auto;
                autoaux.Marca = Interaction.InputBox("Marca: ", "Modificando...", autoaux.Marca);
                autoaux.Modelo = Interaction.InputBox("Modelo: ", "Modificando...", autoaux.Modelo);
                string axo = Interaction.InputBox("Año: ", "Modificando...", autoaux.Axo);
                r = new Regex(@"[\d]{4}");
                if (!r.IsMatch(axo)) throw new Exception("El año debe ser numérico !!!");
                autoaux.Axo = axo;
                string precio = Interaction.InputBox("Precio: ", "Modificando...", autoaux.Precio.ToString());
                if (!Information.IsNumeric(precio)) throw new Exception("El precio debe ser numérico !!!");
                autoaux.Precio = decimal.Parse(precio);
                empresa.ModificarAuto(autoaux);
                Mostrar(dataGridView2, empresa.ListaAutos());
                Mostrar(dataGridView4, av.RetornaAutoVista(empresa.ListaAutos()));

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void button7_Click(object sender, EventArgs e)
        {

            try
            {
                Persona p = dataGridView1.CurrentRow.DataBoundItem as Persona;
                Auto a = dataGridView2.CurrentRow.DataBoundItem as Auto;
                empresa.AsiganaAuto(p,a);
                Mostrar(dataGridView1, empresa.ListaPersonas());
                dataGridView1_RowEnter(null, null);
                //Mostrar(dataGridView4, av.RetornaAutoVista(empresa.ListaAutos()));
                Mostrar(dataGridView4, empresa.RetornaVistaAuto());
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                    Mostrar(dataGridView3, (dataGridView1.SelectedRows[0].DataBoundItem as Persona).RetornaAutos());
                else
                    dataGridView1.Rows.Clear();
            }
            catch (Exception)  { }
        }
    }

    public class Empresa
    {
        List<Persona> lp;
        List<Auto> la;
        public Empresa()  { lp = new List<Persona>();la = new List<Auto>(); }

        public void AgregarPersona(Persona pPersona)

        {
            try
            {
                // Se verifiva si existe el DNI. En caso afirmativo se libera una exception.
                if (lp.Exists(x => x.DNI == pPersona.DNI)) throw new Exception("El DNI ya existe !!!");
                // Se crea un clon de la persona que se recibió en el parámetro de la función y se carga en la lista de personas de la empresa
                lp.Add(new Persona(pPersona.DNI, pPersona.Nombre, pPersona.Apellido));
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }
        public void BorrarPersona(Persona pPersona)
        {

            try
            {
                // Se verifica que el DNI a borrar exista
                if (!lp.Exists(x => x.DNI == pPersona.DNI)) throw new Exception("El DNI que desea borrar no existe !!!");
                // Se busca en la lista de personas de la empresa por el DNI y se remueve de la lista
                lp.Remove(lp.Find(x => x.DNI == pPersona.DNI));
                // TODO: Si posee autos a cada auto hay que pasarle el campo dueño a null
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }
        public void ModificarPersona(Persona pPersona)
        {

            try
            {
                // Se busca la persona a modificar en la lista de personas de la empresa
                Persona paux= lp.Find(x => x.DNI == pPersona.DNI);
                // Se verifica que la persona existe
                if (paux == null) throw new Exception("La persona que desea modificar no existe !!!");
                // Se modifica la persona de la lista de personas de la empresa con los datos que llegaron en la persona que se recibió por parámetro a la función
                paux.Nombre=pPersona.Nombre;paux.Apellido = pPersona.Apellido;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }
        public List<Persona> ListaPersonas()
        {
            // Se crea una lista auxiliar de personas y se crea un clos de cada persona de la lista de personas de la empresa
            List<Persona> lpaux = new List<Persona>();
            foreach(var p in lp)
            {              
                lpaux.Add(new Persona(p.DNI,p.Nombre,p.Apellido));
                // Se pregunta si la persona es dueña de autos y se crean clones de esos autos para cargarselos a la persona
                if(p.RetornaAutos().Count>0)
                {
                    foreach(var a in p.RetornaAutos())
                    {
                        lpaux.Last().CargaAuto(new Auto(a.Patente,a.Marca,a.Modelo,a.Axo,a.Precio));
                    }

                }
            }
            return lpaux;
        }
        public void AgregarAuto(Auto pAuto)

        {
            try
            {
                // Se verifiva si existe la patente. En caso afirmativo se libera una exception.
                if (la.Exists(x => x.Patente == pAuto.Patente)) throw new Exception("La patente ya existe !!!");
                // Se crea un clon del auto que se recibió en el parámetro de la función y se carga en la lista de autos de la empresa
                la.Add(new Auto(pAuto.Patente,pAuto.Marca,pAuto.Modelo,pAuto.Axo,pAuto.Precio));
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }
        public void BorrarAuto(Auto pAuto)
        {

            try
            {
                // Se verifica que la patente a borrar exista
                if (!la.Exists(x => x.Patente == pAuto.Patente)) throw new Exception("El auto que desea borrar no existe !!!");
                // Se busca en la lista de autos de la empresa por la patente y se remueve de la lista de la empresa
                la.Remove(la.Find(x => x.Patente == pAuto.Patente));
                // TODO: Si el auto posee dueño quitar el auto de la lista de autos de esa persona
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }
        public void ModificarAuto(Auto pAuto)
        {
            
            try
            {
                // Se busca el auto a modificar en la lista de autos de la empresa
                Auto aaux = la.Find(x => x.Patente == pAuto.Patente);
                // Se verifica que el auto existe
                if (aaux == null) throw new Exception("El auto que deseamodificar no existe !!!");
                // Se modifica el auto de la lista de autos de la empresa con los datos que llegaron en el auto que se recibió por parámetro a la función
                aaux.Marca = pAuto.Marca; aaux.Modelo = pAuto.Modelo; aaux.Axo = pAuto.Axo; aaux.Precio = pAuto.Precio;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }
        public List<Auto> ListaAutos()
        {
            // Se crea una lista auxiliar retorna clones del los autos que posee en la lista de autos de la empresa
            List<Auto> laaux = new List<Auto>();
            foreach (var a in la)
            {
                laaux.Add(new Auto(a.Patente,a.Marca, a.Modelo, a.Axo,a.Precio));
                Persona auxP = a.RetornaDueno();
                if(auxP!=null)
                {
                    laaux.Last().CargaDueno(new Persona(auxP.DNI,auxP.Nombre,auxP.Apellido));
                }
            }
            return laaux;
        }
        public void AsiganaAuto(Persona pPersona, Auto pAuto)
        {

            try
            {
                 // Se busca el auto en la lista de autos de la empresa y verifico que no posea dueño
                Auto autoempresa = la.Find(x => x.Patente == pAuto.Patente);
                if (autoempresa == null) throw new Exception("El auto no existe !!!");
                // Se corrobora que el auto no posea dueño
                if (autoempresa.RetornaDueno() != null) throw new Exception("El auto ya posee un dueño !!!");
                // Se busca la persona en la lista de personas de la enpresa 
                Persona personaempresa = lp.Find(x => x.DNI == pPersona.DNI);
                if (personaempresa == null) throw new Exception("La persona no existe !!!");
                // Se le carga la persona como dueño del auto
                autoempresa.CargaDueno(personaempresa);
                // Se carga el auto al propietario
                personaempresa.CargaAuto(autoempresa);


            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

        }
        public object RetornaVistaAuto()
        {
            return (from a in ListaAutos() select new {Patente=a.Patente,MArca=a.Marca,Modelo=a.Modelo,
                                                       Axo=a.Axo,Precio=a.Precio,DNI=a.RetornaDueno()==null?"--":a.RetornaDueno().DNI,
                                                       Nombre_Apellido=a.RetornaDueno()==null?"--":a.RetornaDueno().Nombre + " " + a.RetornaDueno().Apellido}).ToList();
        }
    }
    public class Persona
    {
        List<Auto> la;
        public Persona()
        { 
            la = new List<Auto>();
        }
        public Persona(string pDNI, string pNombre, string pApellido) : this()
        {
            DNI = pDNI;Nombre = pNombre;Apellido = pApellido;
        }
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public void CargaAuto(Auto pAuto)
        {
            la.Add(new Auto(pAuto.Patente,pAuto.Marca,pAuto.Modelo,pAuto.Axo,pAuto.Precio));
        }
        public List<Auto> RetornaAutos()
        {
            List<Auto> laaux = new List<Auto>();
            foreach (var a in la) 
            {
                laaux.Add(new Auto(a.Patente,a.Marca,a.Modelo,a.Axo,a.Precio));
            }
            return laaux;
        }
    }
    public class Auto
    { 
        Persona dueno;
        public Auto()
        {
            dueno = null;
        }
        public Auto(string pPatente,string pMarca,string pModelo, string pAxo,decimal pPrecio) :this()
        {
            Patente = pPatente;Marca = pMarca;Modelo = pModelo;Axo = pAxo;Precio = pPrecio;
        }
        public string Patente { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Axo { get; set; }
        public decimal Precio { get; set; }

        public void CargaDueno(Persona pPersona) { dueno = pPersona; }
        public Persona RetornaDueno() { return dueno; }

    }

    public class AutoVista
    {
        public AutoVista()
        { }
        public AutoVista(string pPatente, string pMarca, string pModelo, string pAxo, decimal pPrecio, string pDNI, string pNombreApellido) 
        {
            Patente = pPatente; Marca = pMarca; Modelo = pModelo; Axo = pAxo; Precio = pPrecio;DNI = pDNI;Nombre_Apellido = pNombreApellido;
        }
        public string Patente { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Axo { get; set; }
        public decimal Precio { get; set; }
        public string DNI { get; set; }
        public string Nombre_Apellido { get; set; }

        public List<AutoVista> RetornaAutoVista(List<Auto> pListaAutos)
        {
            List<AutoVista> l = new List<AutoVista>();
            foreach(var a in pListaAutos)
            {
                string DNI,Nombre_Apellido;
                Persona auxP = a.RetornaDueno();
                if (auxP == null)
                {
                    DNI = "--";Nombre_Apellido = "--";
                }
                else { DNI = auxP.DNI;Nombre_Apellido = $"{auxP.Nombre} {auxP.Apellido}"; }

                l.Add(new AutoVista(a.Patente,a.Marca,a.Modelo,a.Axo,a.Precio,DNI,Nombre_Apellido));
            }

            return l;
        }


    }

}

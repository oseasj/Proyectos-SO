using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace planificador
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


        }

        public string asignar_color() {

            string color=null;
            Random num = new Random();

            int valor = num.Next(1, 10);

            switch (valor ) {

                case 1:
                    color = "red";
                    break;
                case 2:
                    color = "blue";
                    break;
                case 3:
                    color = "black";
                    break;
                case 4:
                    color = "yellow";
                    break;
                case 5:
                    color = "orange";
                    break;
                case 6:
                    color = "violet";
                    break;
                case 7:
                    color = "brown";
                    break;
                case 8:
                    color = "beige";
                    break;
                case 9:
                    color = "green";
                    break;
                case 10:
                    color = "pink";
                    break;
            }
            return color;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tblprocesamiento.Rows.Add(100);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }   

        private void btnagregar_Click(object sender, EventArgs e)
        {
            string proceso = txtproceso.Text;
            string requerimiento =txtrequerimiento.Text;
            string color = asignar_color();
            if ((proceso == "") || (requerimiento == ""))
            {
                MessageBox.Show("COMPLETAR LOS CAMPOS");
            }
            else {
                tblprocesos.Rows.Add(proceso, requerimiento, color);
                txtproceso.Text = "";
                txtrequerimiento.Text = "";
            }
        }

        public int fiprocesos()
        {
            int filas = tblprocesos.RowCount;
            return filas;
        }

        public int colprocesos()
        {
            int col = tblprocesos.ColumnCount;
            return col;
        }

        public int fiprocesamiento()
        {
            int fil = tblprocesamiento.RowCount;
            return fil;
        }

        public int colprocesamiento()
        {
            int col = tblprocesamiento.ColumnCount;
            return col;
        }

        public void pintar(String color, int x, int y, DataGridView tabla) 
        {
            Color col = Color.FromName(color);
            tabla.Rows[x].Cells[y].Style.BackColor = col; 
            //----------------------------------------------------------------
            tabla.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            switch (color) 
            {
                case "red":
                case "black":
                case "blue":
                case "orange":
                case "violet":
                case "brown":
                case "green":
                case "gray":
                    tabla.DefaultCellStyle.ForeColor = Color.White;
                   
                    break;
                default:
                  
                    tabla.DefaultCellStyle.ForeColor = Color.Black;
                    break;
            }
        }

        public void escribir_planificacion(String dato, int x, int y, DataGridView tabla)
        {
            tabla[y, x].Value = dato;         
        }

        int xx = 0, yy = 0;
        int d = 0;

        public void procesar()
        {
            if (fiprocesos() - 1 > 0) 
            {
                int estado = 0;
                while ((d <= fiprocesos() - 1) && (estado == 0))
                {
                    int cpu = Convert.ToInt32(tblprocesos[1, d].Value);
                    String nombreProceso = Convert.ToString(tblprocesos[0, d].Value);
                    if (cpu != 0)
                    {
                        estado = 1;
                        Console.WriteLine("VALOR CPU" + cpu);
                        String resta = Convert.ToString(cpu - Convert.ToInt32(txtquantum.Text));
                        int valresta = Convert.ToInt32(resta);
                        if (valresta >= 0) 
                        {
                            tblprocesos[1, d].Value = resta;
                            String color = tblprocesos[2, d].Value.ToString();
                            for (int cant = 1; cant <= Convert.ToInt32(txtquantum.Text); cant++)
                            {
                                escribir_planificacion(nombreProceso, xx, yy, tblprocesamiento);                              
                                pintar(color, xx, yy, tblprocesamiento);

                                if (yy == colprocesamiento() - 1)
                                {
                                    xx++;
                                    yy = -1;
                                }
                                yy++;
                            }
                        }
                        else if (valresta < 0) 
                        {
                            int pendientevalor = Convert.ToInt32(tblprocesos[1, d].Value);
                            String color = tblprocesos[2, d].Value.ToString();//obtiene nombre de color
                            for (int cant = pendientevalor; cant <= pendientevalor; cant++)
                            {

                                escribir_planificacion(nombreProceso, xx, yy, tblprocesamiento);
                                pintar(color, xx, yy, tblprocesamiento);                             
                                if (yy == colprocesamiento() - 1)
                                {
                                    Console.WriteLine("columna : " + yy);
                                    xx++;
                                    yy = -1;
                                }
                                yy++;
                            }
                            tblprocesos[1, d].Value = 0;
                        }
                    }                   
                    d++;
                    if (d == fiprocesos() - 1)
                    {
                        d = 0;
                        estado = 1;
                    }
                }

            }
        }

        private void btnprocesar_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           procesar();
        }
    }
}

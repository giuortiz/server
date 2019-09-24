using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Server
{
    class Grupo {
        public List<Pessoa> Integrantes {get; set;}
        public string Nota { get; set; }
        public string Tema { get; set; }

    }
    class Pessoa
    {
        public string Nome { get; set; }
        public int nUsp { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Grupo RegisteredUsers = new Grupo();
            RegisteredUsers.Integrantes = new List<Pessoa>();

         //   RegisteredUsers.Tema = "Idade Média";
            RegisteredUsers.Integrantes.Add(new Pessoa() {nUsp = 9878079, Nome = "Giuliano Ortiz Goria"});
            RegisteredUsers.Integrantes.Add(new Pessoa() {nUsp = 991490, Nome = "Nicole Wilcox"});
            RegisteredUsers.Integrantes.Add(new Pessoa() {nUsp = 9911520, Nome = "Adrian Martinson" });
            RegisteredUsers.Integrantes.Add(new Pessoa() {nUsp = 9911455, Nome = "Nora Osborn",});
            Grupo RegisteredUsers2 = new Grupo();
            RegisteredUsers2.Integrantes = new List<Pessoa>();
          //  RegisteredUsers2.Tema = "Berrante";
            RegisteredUsers2.Integrantes.Add(new Pessoa() { nUsp = 9878079, Nome = "Indio Zellar", });
            RegisteredUsers2.Integrantes.Add(new Pessoa() { nUsp = 991490, Nome = "Victor Zellar", });
            RegisteredUsers2.Integrantes.Add(new Pessoa() { nUsp = 9911520, Nome = "Monstruosidade Monstruosa" });
            RegisteredUsers2.Integrantes.Add(new Pessoa() { nUsp = 9911455, Nome = "Enzo Zellar", });
            Grupo RegisteredUsers3 = new Grupo();
            RegisteredUsers3.Integrantes = new List<Pessoa>();
        //    RegisteredUsers3.Tema = "Sockets";
            RegisteredUsers3.Integrantes.Add(new Pessoa() { nUsp = 9878079, Nome = "Jairo Justus", });
            RegisteredUsers3.Integrantes.Add(new Pessoa() { nUsp = 991490, Nome = "Como Estamos?", });
            RegisteredUsers3.Integrantes.Add(new Pessoa() { nUsp = 9911520, Nome = "Spider Silva" });
            RegisteredUsers3.Integrantes.Add(new Pessoa() { nUsp = 9911455, Nome = "jorginho Doze", });
            Grupo RegisteredUsers4 = new Grupo();
            RegisteredUsers4.Integrantes = new List<Pessoa>();
          //  RegisteredUsers4.Tema = "e-sports";
            RegisteredUsers4.Integrantes.Add(new Pessoa() { nUsp = 9878079, Nome = "Ingrid Gabrielly", });
            RegisteredUsers4.Integrantes.Add(new Pessoa() { nUsp = 991490, Nome = "Olaff Cornin", });
            RegisteredUsers4.Integrantes.Add(new Pessoa() { nUsp = 9911520, Nome = "Tres Meses" });
            RegisteredUsers4.Integrantes.Add(new Pessoa() { nUsp = 9911455, Nome = "Psyduck da Silva", });
            Grupo RegisteredUsers5 = new Grupo();
            RegisteredUsers5.Integrantes = new List<Pessoa>();
         //   RegisteredUsers5.Tema = "ET Bilu";
            RegisteredUsers5.Integrantes.Add(new Pessoa() { nUsp = 9878079, Nome = "Pato da Silva", });
            RegisteredUsers5.Integrantes.Add(new Pessoa() { nUsp = 991490, Nome = "Frangolino dos Santos", });
            RegisteredUsers5.Integrantes.Add(new Pessoa() { nUsp = 9911520, Nome = "Pintinho Piu" });
            RegisteredUsers5.Integrantes.Add(new Pessoa() { nUsp = 9911455, Nome = "Alex Cuppermano", });

            var grupos = new List<Grupo>();
            grupos.Add(RegisteredUsers);
            grupos.Add(RegisteredUsers2);
            grupos.Add(RegisteredUsers3);
            grupos.Add(RegisteredUsers4);
            grupos.Add(RegisteredUsers5);

            TcpListener server = new TcpListener(IPAddress.Any, 9999);
            // we set our IP address as server's address, and we also set the port: 9999
            server.Start();  // this will start the serve
            Console.WriteLine("Esperando algum doido...");
            while (true)   //we wait for a connection
            {
                TcpClient client = server.AcceptTcpClient();  //if a connection exists, the server will accept it
                Console.WriteLine("Doido chegou...");
                NetworkStream ns = client.GetStream(); //networkstream is used to send/receive messages//sending the message
                string clientMessage = string.Empty;
                byte[] serverMessage = new byte[100];
                while (client.Connected)  //while the client is connected, we look for incoming messages
                {
                    byte[] msg = new byte[1024];     //the messages arrive as byte array
                    ns.Read(msg, 0, msg.Length);   //the same networkstream reads the message sent by the client
                    clientMessage = Encoding.UTF8.GetString(msg).Replace("\0", "");
            
                    //now , we write the message as string
                    var p = new JavaScriptSerializer().Deserialize<Pessoa>(clientMessage);
                    if (p!=null)
                    {
                        if (p.Nome == "Xolis")
                        {
                            var serializer = new JavaScriptSerializer();
                            var serializedResult = serializer.Serialize(grupos);
                            serverMessage = Encoding.Default.GetBytes(serializedResult);

                        }
                        else
                        {
                            serverMessage = Encoding.Default.GetBytes("a");
                        }
                    }
                    ns.Write(serverMessage, 0, serverMessage.Length);
                    if (p != null) {
                        Console.WriteLine($"{p.Nome}");
                    }
                }
            }
        }
    }
}

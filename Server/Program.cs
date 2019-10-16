

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using FirebaseSharp.Portable;



namespace Server
{
    class Grupo
    {
        public List<Pessoa> Integrantes { get; set; }
        public string Nota { get; set; }
        public string Tema { get; set; }

    }
    class Pessoa
    {
        public string Nome { get; set; }
        public int nUsp { get; set; }
        public bool pertenceGrupo { get; set; }


    }
    class Program
    {

        static void Main(string[] args)
        {
       

            //lista de alunos
            List<Pessoa> estudantes = new List<Pessoa>();
            estudantes.Add(new Pessoa() { nUsp = 9878079, Nome = "Giuliano Ortiz Goria", pertenceGrupo = false });
            estudantes.Add(new Pessoa() { nUsp = 991490, Nome = "Nicole Wilcox", pertenceGrupo = false });
            estudantes.Add(new Pessoa() { nUsp = 9911520, Nome = "Adrian Martinson", pertenceGrupo = false });
            estudantes.Add(new Pessoa() { nUsp = 9911455, Nome = "Nora Osborn", pertenceGrupo = false });
            estudantes.Add(new Pessoa() { nUsp = 9878079, Nome = "Gustavo Zellar", pertenceGrupo = false });
            estudantes.Add(new Pessoa() { nUsp = 991490, Nome = "Victor Zellar", pertenceGrupo = false });
            estudantes.Add(new Pessoa() { nUsp = 9911520, Nome = "Marcelo Milka", pertenceGrupo = false });
            estudantes.Add(new Pessoa() { nUsp = 9911455, Nome = "Enzo Zellar", pertenceGrupo = false });
            estudantes.Add(new Pessoa() { nUsp = 9878079, Nome = "Jairo Justus", pertenceGrupo = false });
            estudantes.Add(new Pessoa() { nUsp = 991490, Nome = "Fred Simon", pertenceGrupo = false });
            estudantes.Add(new Pessoa() { nUsp = 9911520, Nome = "Spider Silva", pertenceGrupo = false });
            estudantes.Add(new Pessoa() { nUsp = 9911455, Nome = "Jorge Jackson", pertenceGrupo = false });
            estudantes.Add(new Pessoa() { nUsp = 9878079, Nome = "Ingrid Gabrielly", pertenceGrupo = false });
            estudantes.Add(new Pessoa() { nUsp = 991490, Nome = "Olaff Junior", pertenceGrupo = false });
            estudantes.Add(new Pessoa() { nUsp = 9911455, Nome = "Juão Barnes", pertenceGrupo = false });


            var grupos = new List<Grupo>();
         

            void servidor()
            {
                // Atribuir o IP e a porta
                TcpListener server = new TcpListener(IPAddress.Any, 9999);
                Console.WriteLine("Iniciando servidor...");

               
                try
                {
                    //inicia o servidor
                    server.Start();

            
                }
                catch
                {
                    
                    server.Stop();
                    servidor();

                }
              
                Console.WriteLine("Servidor iniciado...");
              
                Console.WriteLine("Esperando alguma conexão...");
                while (true)   //aguarda conexão
                {
                    TcpClient client = server.AcceptTcpClient();  //aceita a conexão
                    Console.WriteLine("conexão estabelecida...");
                    NetworkStream ns = client.GetStream(); //networkstream é usado para receber e enviar mensagens
                    string clientMessage = string.Empty;
                    byte[] serverMessage = new byte[100];
                    while (client.Connected)  //operações enquanto o cliente está conectado
                    {
                        byte[] msg = new byte[1024];     
                        try
                        {
                            ns.Read(msg, 0, msg.Length);
                        }
                        catch
                        {
                            servidor();
                        }  //recebe a a mensagem em bytes e decodifica
                        clientMessage = Encoding.UTF8.GetString(msg).Replace("\0", "");

                        try
                        {
                            var p = new JavaScriptSerializer().Deserialize<Pessoa>(clientMessage); //converte a mensagem em objeto
                            if (p != null)
                            {
                                if (p.Nome == "lista grupos")
                                {
                                    //envia a lista de grupos para o app
                                    Console.WriteLine("Enviando lista de Grupos...");

                                    var serializer = new JavaScriptSerializer();
                                    var serializedResult = serializer.Serialize(grupos);
                                    serverMessage = Encoding.Default.GetBytes(serializedResult);

                                }
                                else if (p.Nome == "lista alunos")
                                {
                                    //envia a lista de alunos para o app
                                    Console.WriteLine("Enviando lista de alunos...");

                                    var serializer = new JavaScriptSerializer();
                                    var serializedResult = serializer.Serialize(estudantes);
                                    serverMessage = Encoding.Default.GetBytes(serializedResult);
                                }
                                else
                                {
                                    //adiciona um novo grupo à lista de grupos
                                    var s = new JavaScriptSerializer().Deserialize<Grupo>(clientMessage);
                                    if (s != null)
                                    {
                                        grupos.Add(s);
                                        Console.WriteLine("Lista Grupo Adicionado...");
                                        Console.WriteLine(s.Integrantes[0].Nome);
                                    }
                                }
                            }
                        }
                        catch
                        {


                        }


                        try
                        {
                            ns.Write(serverMessage, 0, serverMessage.Length);
                            Console.WriteLine("Lista enviada...");

                        }
                        catch
                        {
                            server.Stop();
                            servidor();

                        }


                    }
                }
            }
            servidor();
        }
    }
}

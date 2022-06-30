/*
 * Created by SharpDevelop.
 * User: Lautaro
 * Date: 4/4/2022
 * Time: 13:50
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace TPF
{
	class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			
			ArbolGeneral DNS = new ArbolGeneral("",null);
			Menu(DNS);
			
	
			
			
			// TODO: Implement Functionality Here
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
		

		public static void Menu(ArbolGeneral DNS)
		{
			Console.WriteLine("BIENVENIDOS Al SISTEMA DNS ");
			Console.WriteLine("--------------------------------------------------------");
			int opcion,opcion2;
		
			do{

				Console.Write("\n1. Ingresar a Administración " +
				              "\n2. Ingresar a Consultas." +
				              "\n0. Salir\nIngrese una opción: ");
				
				opcion2 = int.Parse(Console.ReadLine());
				try{
					confirmarOpcion(opcion2,1);
				}
				catch(OpcionInvalida){
							Console.WriteLine("\nIngrese una opcion valida por favor"); //Para que salga del while,se repita el pedido de instrucciones
										   
				}
				
				switch(opcion2)
				{
						case 1:{  //Modulo de Administracion
							do{
								
								Console.Clear();
								Console.Write("\n1. Ingreso y almacenamiento de nombres de dominio correspondientes a equipos conectado a la red " +
								              "\n2. Eliminación de nombres de equipos." +
								              "\n0. Volver al menu principal" +
								              "\nIngrese una opción: ");
								
								opcion = int.Parse(Console.ReadLine());
								bool auxmenu=true;
								Console.Clear();
								
								while(auxmenu){
									try{
										confirmarOpcion(opcion,1);
										auxmenu=false;
										
										switch(opcion)
										{
											case 1: 
												{ 
													
													Console.WriteLine("Proceda a ingresar los datos  para agregar el equipo a la red");
													Console.Write("Ingrese el dominio ej.'www.Wikipedia.org': ");
													string dominio = Console.ReadLine();
													Console.Write("Direccion IP : ");
													string ip = Console.ReadLine();
													Console.Write("Servicio que provee (WWW | FTP | DNS | ROUTING ) : ");
													string servicio = Console.ReadLine();
													
												
													ArbolGeneral nuevoDominio =new ArbolGeneral(dominio,null);
													DNS.agregarDominio(dominio,ip,servicio);
													break;
												}
											case 2:
												{//Metodo eliminar
													Console.WriteLine("Ingrese el dominio o subdominio a eliminar: ");
													string dominio= Console.ReadLine();
													DNS.eliminarUrl(dominio);
													Console.ReadKey(true);
													break;
												}
											case 0 : break;
												
										}
										
									}
									
									catch(OpcionInvalida){
										Console.WriteLine("Ingrese una opcion valida por favor");
										break;    //Para que salga del while,se repita el pedido de instrucciones
									}
								}
								
							}
							
							while(opcion != 0);
							break;
							
						}
						case 2:{
							do{
								
								Console.Clear();
								Console.Write("\n1.Ingreso el nombre del dominio para ver su direccion IP y los servicios que provee. " +
								              "\n2. Ingrese un subdominio para ver todos los equipos que dependen de él" +
								              "\n3. Ingrese una profundidad para ver  las cantidades de dominios de nivel superior, subdominios y equipos ubicados a dicha profundidad."+
								              "\n0. Volver al menu principal" +
								              "\nIngrese una opción: ");
								

								opcion = int.Parse(Console.ReadLine());
								bool auxmenu=true;
								Console.Clear();
								
								while(auxmenu){
									try{
										confirmarOpcion(opcion,2);
										auxmenu=false;
										
										switch(opcion)
										{
											case 1:
												{ // Metodo ver ip y servicio
													Console.WriteLine("Ingrese el dominio que necesite ver su direccion IP y los servicios que proveé: ");
													string dominio= Console.ReadLine();
													DNS.devolverIpyServicios(dominio);
													Console.ReadKey(true);
													break;
												}
											case 2:
												{//Metodo ver subdominios dependientes
													Console.WriteLine("Ingrese un subdominio para ver todos los equipos que dependen de él: ");
													string dominio= Console.ReadLine();
													DNS.imprimirSubdominios(dominio);
													Console.ReadKey(true);
													break;
												}
											case 3:
												{
													//metodo profundidad
													Console.WriteLine("Ingrese la profundidad para ver las cantidades de dominios de nivel superior,subdominios y equipos ubicados a dicha profundidad: ");
													int dominio= int.Parse(Console.ReadLine());
													DNS.profundidadNodos(dominio);
													Console.ReadKey(true);
													break;
												}
												
											case 0 : break;
												
										}	
										
									}
									
									catch(OpcionInvalida){
										Console.WriteLine("Ingrese una opcion valida por favor");
										break;    //Para que salga del while,se repita el pedido de instrucciones
									}
								}
								
							}
							
							while(opcion != 0);
							break;
						}
						case 0:{
							break;
						}
				}
					
				
			}
			while(opcion2 !=0);      //Tenerlo en un while me obliga a elegir la opcion correcta siempre
			Console.WriteLine();
			Console.WriteLine("Fin del progrma");
			
		}
		
		
		
		public static void confirmarOpcion(int n,int opcion)
		{
			// modificar por si toca cualquier tecla
			
			if(opcion == 1){ //Caso "menu principal"
				if(n <0 || n >2 ){
					throw new OpcionInvalida();
				}
			}
			else if (opcion==2)
			{
				if(n <0 || n >3 ){
					throw new OpcionInvalida();
				}
				
			}
			
		}
	}
	
	
	//			
////			string prueba= "www.wikipedia.org";
////			string ip= "192.168.0.0.";
////			string servicio= "DNS";
////			
////			Console.WriteLine(string.Compare(ip,ip));
////			
////			string[] valores= prueba.Split('.');
////			Array.Reverse(valores);
////			foreach(var g in valores)
////				Console.WriteLine(g);
////			
////			ArbolGeneral a= new ArbolGeneral( valores[0]);
////			ArbolGeneral b= new ArbolGeneral( valores[1]);
////			ArbolGeneral c= new ArbolGeneral( valores[2]);
//			DNS.agregarDominio("www.facebook.com","1","web");
//						DNS.agregarDominio("www.google.com","1","web");
//			DNS.agregarDominio("wwe.facebook.com","2","web");
//			DNS.agregarDominio("wwe.es.facebook.com","2","web");
//			DNS.agregarDominio("www.facebook.org","3","web");
//			DNS.profundidadNodos(2);
//			DNS.devolverIpyServicios("www.facebook.org");
////			DNS.agregarDominio("www.es.facebook.com","1","web");
////						DNS.agregarDominio("www.edds.facebook.com","1","web");
////									DNS.agregarDominio("www.es.river.facebook.com","1","web");
////			DNS.agregarDominio("www.google.com","2","web");
////			DNS.agregarDominio("wwee.facebook.org","1","web");
////			DNS.agregarDominio("www.google.org","1","web");
//			DNS.porNivelesConSeparacion();
////			DNS.eliminarUrl("com");
////			Console.WriteLine("s");
////			DNS.porNivelesConSeparacion();
//////			DNS.agregarDominio("www.Wikipedia.com","1","web");
////						DNS.porNivelesConSeparacion();
////						Console.WriteLine("------------------ ");
//////						DNS.eliminarUrl("com");
////						DNS.imprimirSubdominios("facebook");
////						DNS.eliminarUrl("es");
//////												DNS.eliminarUrl("www.es.Wikipedia.com");
////						Console.WriteLine("sin nada");
////												DNS.imprimirSubdominios("facebook");
////												DNS.porNivelesConSeparacion();
//////						DNS.porNivelesConSeparacion();
//////						DNS.devolverIpyServicios("www.google.com");
////			
//////						DNS.porNivelesConSeparacion();
//////			DNS.agregarDominio("www.Wikipedia.org","1","web");
//////			DNS.agregarDominio("weew.Wikipedia.org","1","web");
////////			DNS.agregarDominio("ee.Wikipedia.org","1","web");
//////			DNS.agregarDominio("ee.Wikipedia.com","1","web");
//////			DNS.porNivelesConSeparacion();
//////			DNS.agregarDominio("www.Wikipediña.org","1","web");
//////			DNS.porNivelesConSeparacion();
//////			DNS.agregarDominio("e.Wikipedia.com","1","web");
//////			DNS.porNivelesConSeparacion();
//////			DNS.agregarDominio("www.Weikipedia.com","1","web");
//////						DNS.porNivelesConSeparacion();
//////			DNS.agregarDominio("www.Weikipedia.es","1","web");
//////			DNS.porNivelesConSeparacion();
//////			DNS.agregarDominio("es.google.ar","1","web");
////			////			DNS.agregarHijo(a);
////			////			DNS.agregarHijo(b);
////			////			DNS.agregarHijo(c);
//////			DNS.porNivelesConSeparacion();
////
////
//			
}
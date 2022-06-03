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
			
			ArbolGeneral DNS = new ArbolGeneral("");
			
			string prueba= "www.wikipedia.org";
			string ip= "192.168.0.0.";
			string servicio= "DNS";
			
			Console.WriteLine(string.Compare(ip,ip));
			
			string[] valores= prueba.Split('.');
			Array.Reverse(valores);
			foreach(var g in valores)
				Console.WriteLine(g);
		
			ArbolGeneral a= new ArbolGeneral( valores[0]);
			ArbolGeneral b= new ArbolGeneral( valores[1]);
			ArbolGeneral c= new ArbolGeneral( valores[2]);
			DNS.agregarDominio("www.Wikipedia.org","1","web");
//			DNS.agregarHijo(a);
//			DNS.agregarHijo(b);
//			DNS.agregarHijo(c);
			DNS.porNivelesConSeparacion();
			
			
			
	
			
			
	
			
			
			
			
			
			
			// TODO: Implement Functionality Here
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}
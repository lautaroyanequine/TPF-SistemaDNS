using System;
using System.Collections.Generic;

namespace TPF
{
	public class ArbolGeneral
	{
		
		private string textoEtiqueta,direccionIP,serviciosQueProvee;
	
		private List<ArbolGeneral> hijos = new List<ArbolGeneral>();

		public ArbolGeneral(string textoEtiqueta) {
			this.textoEtiqueta = textoEtiqueta;
		}
	
		public string getTextoEtiquetaRaiz() {
			return this.textoEtiqueta;
		}
		public void setTextoEtiquetaRaiz(string dat){
			this.textoEtiqueta = dat;
		}

		public string getDireccionIP() {
			return this.direccionIP;
		}
		public void setDireccionIP(string dat){
			this.direccionIP = dat;
		}
		public string getServicios() {
			return this.serviciosQueProvee;
		}
		public void setServicios(string dat){
			this.serviciosQueProvee = dat;
		}
	
		public List<ArbolGeneral> getHijos() {
			return hijos;
		}
	
		public void agregarHijo(ArbolGeneral hijo) {
			this.getHijos().Add(hijo);
		}
	
		public void eliminarHijo(ArbolGeneral hijo) {
			this.getHijos().Remove(hijo);
		}
	
		public bool esHoja() {
			return this.getHijos().Count == 0;
		}
		
	
		public int altura() {
			
			if(this.esHoja())
				return 0;
			else{
				int altMax = -1;
				foreach(ArbolGeneral hijo in this.hijos){
					if(hijo.altura() >altMax)
						altMax =hijo.altura();
					}
				return altMax + 1;
			}
		}
		
		
		//RECORRIDOS    - kAlgoritmos que nos permiten visitar cada nodo una vez

		public void preorden(){
			// primero procesamos raiz
			Console.Write(this.textoEtiqueta + " ");
			
			// luego los hijos recursivamente
			foreach(var hijo in this.hijos)
				hijo.preorden();
		}
		
		public void postorden(){
			// primero los hijos recursivamente
			foreach(var hijo in this.hijos)
				hijo.postorden();
			
			// luego procesamos raiz
			Console.Write(this.textoEtiqueta + " ");
		}
		
		public void inorden(){
			// primero hijo izquierdo recursivamente
			if(!this.esHoja())
				this.hijos[0].inorden();
			
			// luego raiz (caso base)
			Console.Write(this.textoEtiqueta + " ");
			
			// por ultimo, los restantes hijos recursivamente
			for(int i = 1; i <= this.hijos.Count - 1; i++)
				this.hijos[i].inorden();
		}
		
		public void porNiveles(){
			Cola c = new Cola();
			ArbolGeneral arbolAux;
			
			c.encolar(this);
			while(!c.esVacia()){
				arbolAux = c.desencolar();
				
				//Proceso el textoEtiqueta
				Console.Write(arbolAux.textoEtiqueta + " ");
				
				foreach(var hijo in arbolAux.hijos)
					c.encolar(hijo);				
			}			
		}		
		
		public void porNivelesConSeparacion(){
			Cola c = new Cola();
			ArbolGeneral arbolAux;
			
			int nivel = 0;
			
			c.encolar(this);
			c.encolar(null);
			
			Console.Write("Nivel " + nivel + ": ");
			
			while(!c.esVacia()){
				arbolAux = c.desencolar();
				
				if(arbolAux == null){
					if(!c.esVacia()){
						nivel++;
						Console.Write("\nNivel " + nivel + ": ");
						c.encolar(null);
					}						
				}
				else{
					Console.Write(arbolAux.textoEtiqueta + " ");
				
					foreach(var hijo in arbolAux.hijos)
						c.encolar(hijo);
				}
			}
		}		

		public int ancho(){
			Cola c = new Cola();
			ArbolGeneral arbolAux;
			
			int anc= 0; //Lleva la cuenta del ancho del arbol
			int contNodos= 0 ; //Cuenta los Arboles_Generales porNiveles nivle
			
			c.encolar(this);
			c.encolar(null);
			
			
			while(!c.esVacia()){
				arbolAux = c.desencolar();
				
				if(arbolAux == null){
					
					//Me voy quedando con el ancho
					if(contNodos>anc)
						anc=contNodos;
					//reseteo contador
					
					contNodos=0;
					
					if(!c.esVacia()){
						
						c.encolar(null);
					}						
				}
				else{
					
					//Procesamos
					contNodos++;
				
					foreach(var hijo in arbolAux.hijos)
						c.encolar(hijo);
				}
			}
			return anc;
		}		

		public int nivel(ArbolGeneral arbol ){
			Cola c = new Cola();
			ArbolGeneral arbolAux;
			ArbolGeneral arbolRecibido= arbol;
			
			int niv=0;
			
			c.encolar(this);
			c.encolar(null);
			
			
			while(!c.esVacia()){
				arbolAux = c.desencolar();
				
				if(arbolAux == null){
										
					niv++;
					
					if(!c.esVacia()){
						
						c.encolar(null);
					}						
				}
				else{
					
					if(arbolAux==arbolRecibido){
						return niv;
					}

				
					foreach(var hijo in arbolAux.hijos)
						c.encolar(hijo);
				}
			}
			return niv;
		}			
		
		
		public void agregarDssominio(string dominio,string i,string ser){
		
			
			string ip= i;
			string servicio=ser;
			string[] valores= dominio.Split('.');
			Array.Reverse(valores);
			bool siExisteEnElNivel=false,siCoincideDominio=false;
			Cola c = new Cola();
			ArbolGeneral arbolAux,arbolAuxPadre=this;
			int nivel = -1;
			
			
			
			//Encolo directamente los hijos,ya que el nodo raiz no tiene dato valido
//			foreach( ArbolGeneral hijo in this.getHijos())
//				c.encolar(hijo);
			c.encolar(this);
			     
			c.encolar(null);
			
			Console.Write("Nivel " + nivel + ": ");
			
			while(!c.esVacia()){
				arbolAux = c.desencolar();
				
				
				if(arbolAux == null)
				{
					if(!c.esVacia()){
						if( nivel< valores.Length){
							if(!siExisteEnElNivel)
							{
								if(nivel == -1)
								{
									ArbolGeneral subDominio= new ArbolGeneral(valores[nivel+1]);
									arbolAuxPadre.agregarHijo(subDominio);
									c.encolar(subDominio);
								}
								else{
									ArbolGeneral subDominio= new ArbolGeneral(valores[nivel]);
									arbolAuxPadre.agregarHijo(subDominio);
									c.encolar(subDominio);
								}
							//Esto antes q encole null y suba de nivel
							}
						}
					
						c.encolar(null);
						nivel++;
						Console.Write("\nNivel " + nivel + ": ");

					}						
				}
				else{
//					arbolAuxPadre=arbolAux;
					
					//Proceso el dato
					if(nivel>=0 && nivel< valores.Length)
					{
					if(string.Compare( arbolAux.textoEtiqueta,valores[nivel]) == 0  )
					{
						siExisteEnElNivel=true;
						siCoincideDominio=true;
						arbolAuxPadre=arbolAux;
					}
					else
					{
						siCoincideDominio=false;
//						ArbolGeneral subDominio= new ArbolGeneral(valores[nivel]);
//						arbolAuxPadre.agregarHijo(subDominio);
					}
					

						
					if(siCoincideDominio)
					{
						
//						foreach(var hijo in arbolAux.hijos)
//						c.encolar(hijo);
						if(nivel==valores.Length-1)
							break;
						ArbolGeneral subDominio= new ArbolGeneral(valores[nivel+1]);
						arbolAuxPadre.agregarHijo(subDominio);
					}
					
					}
					
					
					if(this.esHoja()){
						//Si es hoja agrego el subdominio de mayor importancia directamente ej org.
					
						ArbolGeneral subDominio= new ArbolGeneral(valores[nivel+1]);
						arbolAuxPadre.agregarHijo(subDominio);
						c.encolar(subDominio);
						siExisteEnElNivel=true;
					}
					else{
						foreach(var hijo in arbolAux.hijos)
							c.encolar(hijo);
					}
						
						
				}
			}

		}
		
		
		public void agregarDominio(string dominio,string i,string ser){
			
			string ip= i;
			string servicio=ser;
			string[] valores= dominio.Split('.');
			Array.Reverse(valores); //Doy vuelta los valores del array para que coincidan los niveles con los indices.
			bool siCoincideDominio=false;
			Cola c = new Cola();
			ArbolGeneral arbolAux,arbolAuxPadre=this;
			int nivel = -1;
			
			
			
			c.encolar(this);
			c.encolar(null);
			
			
			while(!c.esVacia()){
				arbolAux = c.desencolar();
				
				if(arbolAux == null){
					if(!c.esVacia()){
						nivel++;
						if(nivel>=0) //Si no es la raiz
						{

							if(!c.contiene(valores[nivel]) ){ //Chequeo si en el nivel existe el subdominio a agregar
								//si no existe lo agrego y encolo antes de  encolar el null
							ArbolGeneral subDominio= new ArbolGeneral(valores[nivel]);
							arbolAuxPadre.agregarHijo(subDominio);
							c.encolar(subDominio);
							}
						}
					
					
						Console.Write("\nNivel " + nivel + ": ");
						c.encolar(null);
					}						
				}
				else{
					
					//Proceso el dato
					if(nivel>=0 && nivel< valores.Length) //Se aseguro que no compare la rai y que nivel no sea mayor a la longitud del array valores
					{
						if(string.Compare( arbolAux.textoEtiqueta,valores[nivel]) == 0  ) //Si en el nivel existe el subdominio
						{
					
							siCoincideDominio=true; 
							arbolAuxPadre=arbolAux; //Pasa a ser el padre,ya que el proximo subdominio se le asignara a este
						}
						else
							siCoincideDominio=false;
					}
					
					//Si el subdominio ya existe
					
					if(siCoincideDominio)
					{
						bool auxiliar=false;
						if(nivel>=valores.Length-1 ) //Si no es la ultima posicion del array,ejemplo si es www,no se le puede agregar un subdominio
						{
							arbolAux.setDireccionIP(ip);
							arbolAux.setServicios(servicio);
							Console.WriteLine(arbolAux.getDireccionIP());
							break;
						};
						//Recorro los subdominios del padre para chequear si ya tiene ese subdominio o no
						List<ArbolGeneral> hijos= arbolAuxPadre.getHijos();
						foreach(ArbolGeneral ar in hijos)
						{
							if(ar.getTextoEtiquetaRaiz()==valores[nivel+1]){
								auxiliar=true;
								break;}
							
						}
						if(!auxiliar){
							//En caso q no tenga el subdominio lo agrego
						ArbolGeneral subDominio= new ArbolGeneral(valores[nivel+1]);
						arbolAuxPadre.agregarHijo(subDominio);
					
						}
					}
					if(arbolAux.esHoja())
					{
						//Si es hoja no hace falta ver si existe el subdominio a agregar,se lo agrega directamente
						if(nivel>=valores.Length-1 ) //Si no es la ultima posicion del array,ejemplo si es www,no se le puede agregar un subdominio
						{
							arbolAux.setDireccionIP(ip);
							arbolAux.setServicios(servicio);
							Console.WriteLine(arbolAux.getDireccionIP());
								break;
						}
						
						ArbolGeneral subDominio= new ArbolGeneral(valores[nivel+1]);
						arbolAuxPadre.agregarHijo(subDominio);
						c.encolar(subDominio);
						
					}
					else{
						if(nivel==-1) //Me aseguro que agregue los hijos de la raiz<
						{
							
							foreach(var hijo in arbolAux.hijos)
								c.encolar(hijo);
						}
						else
							if(siCoincideDominio){// Encolo solo los subdominios correspondientes para que no haya problema de ejecucion.
							foreach(var hijo in arbolAux.hijos)
								c.encolar(hijo);
						}
						
					}
		
				}
			}
		}		
		
		/*
		 1. Encolar raiz y null
		 2. Si la raiz es hoja,agrego el subdominio directamente. Agregarhijo a la raiz.
		 2.1 Si la raiz no es hoja. Encolo a sus hijos, serian subdominios tipo ORG,COM,ES etc.
		 3. Si la raiz es hoja, cuando desencole el primer null subo nivel y encolo proximo null.
		 3.1 Si la raiz no es hoja,antes de encolar el proximo null y subir de nivel,chequeo si existe ya el subdominio a agregar,si existe agrego null y subo de nivel,de caso contrario no
		 
		 
		 */


	}
	
		
}
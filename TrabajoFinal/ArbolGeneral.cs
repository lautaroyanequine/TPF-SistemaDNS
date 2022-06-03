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
			return this.textoEtiqueta;
		}
		public void setDireccionIP(string dat){
			this.textoEtiqueta = dat;
		}
		public string getServicios() {
			return this.textoEtiqueta;
		}
		public void setServicios(string dat){
			this.textoEtiqueta = dat;
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
			Cola<ArbolGeneral> c = new Cola<ArbolGeneral>();
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
			Cola<ArbolGeneral> c = new Cola<ArbolGeneral>();
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
			Cola<ArbolGeneral> c = new Cola<ArbolGeneral>();
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
			Cola<ArbolGeneral> c = new Cola<ArbolGeneral>();
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
		
		
		public void agregarDominio(string dominio,string i,string ser){
			
			
			string ip= i;
			string servicio=ser;
			string[] valores= dominio.Split('.');
			Array.Reverse(valores);
			bool siExisteEnElNivel=false,siCoincideDominio=false; 
			
			
			Cola<ArbolGeneral> c = new Cola<ArbolGeneral>();
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
						nivel++;
						Console.Write("\nNivel " + nivel + ": ");
						c.encolar(null);
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
					
					if(!siExisteEnElNivel)
					{
						ArbolGeneral subDominio= new ArbolGeneral(valores[nivel]);
						arbolAuxPadre.agregarHijo(subDominio);
						c.encolar(subDominio);
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
					}
					else{
						foreach(var hijo in arbolAux.hijos)
							c.encolar(hijo);
					}
						
						
				}
			}

		}


	}
	
		
}
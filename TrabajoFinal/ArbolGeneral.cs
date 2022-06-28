using System;
using System.Collections.Generic;

namespace TPF
{
	public class ArbolGeneral
	{
		
		
		private string textoEtiqueta,direccionIP,serviciosQueProvee;
		private ArbolGeneral padre;
		
		private List<ArbolGeneral> hijos = new List<ArbolGeneral>();

		public ArbolGeneral(string textoEtiqueta,ArbolGeneral padre) {
			this.textoEtiqueta = textoEtiqueta;
			this.padre=padre;
		}
		
		public void setPadre(ArbolGeneral padre){this.padre=padre;}
		public ArbolGeneral getPadre(){return this.padre;}
		public void setTextoEtiquetaRaiz(string etiqueta){this.textoEtiqueta = etiqueta;}
		public string getTextoEtiquetaRaiz() {return this.textoEtiqueta;}
		public void setDireccionIP(string ip){this.direccionIP = ip;}
		public string getDireccionIP() {return this.direccionIP;}
		public void setServicios(string servicio){this.serviciosQueProvee = servicio;}
		public string getServicios() {return this.serviciosQueProvee;}
		public List<ArbolGeneral> getHijos() {return hijos;}
		public void eliminarHijo(ArbolGeneral hijo) {this.getHijos().Remove(hijo);}
		public bool esHoja() {return this.getHijos().Count == 0;}
		public void agregarHijo(ArbolGeneral hijo) {
			this.getHijos().Add(hijo);
			hijo.setPadre(this);
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
			int contNodos= 0 ; //Cuenta los Arboles_Generales porNiveles 
			c.encolar(this);
			c.encolar(null);
			while(!c.esVacia()){
				arbolAux = c.desencolar();
				if(arbolAux == null){

					if(contNodos>anc)					//Me voy quedando con el ancho
						anc=contNodos;
					contNodos=0;					//reseteo contador
					if(!c.esVacia()){
						c.encolar(null);
					}
				}
				else{
					contNodos++;
					foreach(var hijo in arbolAux.hijos)
						c.encolar(hijo);
				}
			}
			return anc;
		}

		
		public void agregarDominio(string dominio,string i,string ser){
			
			string ip= i,servicio=ser;
			string[] valores= dominio.Split('.'); //Separo el string en partes por el "."
			Array.Reverse(valores); //Doy vuelta los valores del array para que coincidan los niveles con los indices.
			bool siCoincideDominio=false;//me indica si el dominio a agregar ya existe
			Cola c = new Cola();
			ArbolGeneral arbolAux,arbolAuxPadre=this; //Se tiene una referencia al arbolpadre para agregarle el subdominio
			int nivel = -1; //Inicializo el nivel en -1 ,para que coincida posiciones del nivel con el array de strings
			c.encolar(this);
			c.encolar(null);
			
			while(!c.esVacia()){
				arbolAux = c.desencolar();
				if(arbolAux == null){
					if(!c.esVacia()){
						nivel++;
						if(nivel>=0) //Si no es la raiz
						{
							if(!c.contiene(valores[nivel]) ){ //Chequeo si en el nivel existe el subdominio a agregar si no existe lo agrego y encolo antes de  encolar el null,asi se agrega en el mismo nivel
								ArbolGeneral subDominio= new ArbolGeneral(valores[nivel],arbolAuxPadre);
								arbolAuxPadre.agregarHijo(subDominio);
								c.encolar(subDominio);
							}
						}
						c.encolar(null);
					}
				}
				else{
					if(nivel>=0 && nivel< valores.Length) // Caso  si ya procese la raiz y que no haya procesado los valores a agregar
					{
						if(string.Compare( arbolAux.textoEtiqueta,valores[nivel]) == 0  ) //Si en el nivel existe el subdominio
						{
							siCoincideDominio=true;
							arbolAuxPadre=arbolAux; //Pasa a ser el padre,ya que el proximo subdominio se le asignara a este
						}
						else
							siCoincideDominio=false;
					}
					if(siCoincideDominio)					//Si el subdominio ya existe
					{
						bool auxiliar=false;
						if(nivel>=valores.Length-1 ) //Si no es la ultima posicion del array,ejemplo si es www,no se le puede agregar un subdominio
						{
							if(string.Compare(valores[nivel],arbolAux.getTextoEtiquetaRaiz())==0){
							arbolAux.setDireccionIP(ip);
							arbolAux.setServicios(servicio);
							break;} //Ya agregue todo el dominio,termina la ejecucion
						}
						//Recorro los subdominios del padre para chequear si ya tiene ese subdominio o no
						List<ArbolGeneral> hijos= arbolAuxPadre.getHijos();
						foreach(ArbolGeneral ar in hijos)
						{
							if(ar.getTextoEtiquetaRaiz()==valores[nivel+1])
								auxiliar=true;
						}
						if(!auxiliar){ 							//En caso q no tenga el subdominio lo agrego
							ArbolGeneral subDominio= new ArbolGeneral(valores[nivel+1],arbolAuxPadre);
							arbolAuxPadre.agregarHijo(subDominio);
						}
					}
					
					if(arbolAux.esHoja()) 					//Caso si el subdominio es hoja
					{
						
						if(nivel>=valores.Length-1 ) //Si es la ultima posicion le agrego los datos IP y servicios
						{
							if(string.Compare(valores[nivel],arbolAux.getTextoEtiquetaRaiz())==0){
							arbolAux.setDireccionIP(ip);
							arbolAux.setServicios(servicio);
							break;}
						}
						//Si no es la ultima posicion,chequeo si el proximo subdominio a agregar ya existe.Si existe no lo agrego
						List<ArbolGeneral> hijos= arbolAuxPadre.getHijos();
						bool auxiliar=false;
						foreach(ArbolGeneral ar in hijos)
						{
							if(nivel<valores.Length-1){
							if(ar.getTextoEtiquetaRaiz()==valores[nivel+1])
								auxiliar=true;
							}
						}	
						if(!auxiliar)
						{
							if(nivel<valores.Length-1){
							ArbolGeneral subDominio= new ArbolGeneral(valores[nivel+1],arbolAuxPadre);		//Si el subdominio a agregar no existe,entonces lo agrego
							arbolAuxPadre.agregarHijo(subDominio);
							c.encolar(subDominio);}
						}
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
								c.encolar(hijo);  //Asi encolo el hijo agregado a la raiz
						}
						
					}
					
				}
			}
		}
		
				/*		1.Separo el string en partes por el "."
		2.Doy vuelta los valores del array para que coincidan los niveles con los indices.
		2.1Se tiene una referencia al arbolpadre para agregarle el subdominio
		
		3. Realizo un recorrido de niveles
		3.1 Inicializo el nivel en -1 ,para que coincida posiciones del nivel con el array de strings
		3.2 Si el arbol es hoja,entonces chequeo si el proximo subdominio a agregar ya existe. si no existe lo agrego y lo encolo
		3.2.1 Si el arbol es hoja y ademas es la ultima posicion(ejemplo un WWW) le asigno la direccion IP y los servicios que provee
		3.3 Me aseguro que el arbol raiz("") y el subdominio que voy a agregar una variante encole a todos sus subdominios
		
		3.4 Cuando desencolo un null( Recorri todo el nivel)
		Si la cola no esta vacia:
				1.incremento el nivel
				2.Chequeo si no es la raiz
					2.1 Chequeo si en el nivel ya existe el subdominio a agregar. Si no existe lo agrego (para que esten en el mismo nivel)
					2.2 Encolo un null
		3.5 Si lo que desencolo no es la raiz ni un null
			3.5.1 Si el no es la raiz y si el nivel es menor a la longitud del array.
				3.5.1.1 Me fijo si es igual al subdominio a encolar
					Si es igual cambia la referencia al arbol padre al arbol deseconola e informo que ya existe el subdominio
			3.5.2 Si el subdomio ya existe,me fijo si es la ultima posicion del array de valores,si es la ultima posicion le asigno la IP y servicios
					 si no 	Recorro los subdominios del padre para chequear si ya tiene ese subdominio o no(tambien para agregar variantes)
					 	Si el padre ya tiene ese subdominio salgo de la iteracion si no lo agrego
			
 
			 */
			
			//" Preparo el terreno"
			/*  El metodo agregarDominio recibe el dominio entero que se quiere agregar,la direccion ip y servicio que se le va a asginar a la hoja de la url(ej."www)
			 * Utilizo la funcion split,separando cada string por el ".",que me devuelve un array de strings,cada posicion con cada subdominio
			 * Doy vuelta los valores del array para que coincidan los niveles con los indices.
			 * Se tiene una variable booleana que me indica si el dominio a agregar ya existe
			 * Se tiene dos referencias de arboles,uno apunto al arbol que desencolo y otro al padre que le tendria que agregar el subdominio
			 * Inicializo el nivel en -1 ,para que coincida posiciones del nivel con el array de strings. Solucionamo ese problema, la raiz del arbol no tiene q ser procesada ya q no tiene dato de valor
			 * */
			//Recorrido por niveles
			/* Cada arbol que desencolo,primero chequeo si es null
			 * en el caso que no sea 
			 * 			Me fijo si el nivel es mayor o igual a 0(quiere decir que ya procese la raiz) y que el nivel sea menor a la longitud del array de subdominios a agregar,si es mayor significa que ya lo procese
			 * 				En el caso que se cumpla esto,chequeo si el subdominio a agregar es igual al texto del arboldesencolado(es decir si esxiste en el nivel)
			 * 					Si esto sucede pongo true en coincide el domionio y la referencia al arbol padre seria el arbol que contiene el subdominio,ya q el proximo elemetno se le agrega a este
			 * 					Si no sucede queda falso el subdominio
			 * 			Si existe el subdominio en el nivel
			 * 				Chequeo si el nivel coincide con la ultima psocion del array
* 								de ser asi compara las estiquetas,si coinciden,signifca que es el ultimo elemento a agregar y por lo tanto le asigno ip y serviciso y termino la ejecucion
* 						
			 * 				Recorro los subdominios del padre para chequear si ya tiene ese subdominio o no
			 * 					Si lo tiene no hago nada
			 * 					Si no lo tiene lo agrego
			 * 			Si el arbol es hoja
			 * 					
			 * 				Recorro los subdominios del padre para chequear si ya tiene ese subdominio o no
			 * 					Si lo tiene no hago nada
			 * 					Si no lo tiene lo agrego
			 * 
			 * 			Me aseguro de encolar los hijos de la raiz y los subdominios que coinciden(por si se quiere agregar variante de url)
			 * 
			 * 
			 * Si es null
			 * 			Si la cola no esta vacia(para evitar loop)
			 * 				subo el nivel
			 * 				antes de encolar el null me fijo
			 * 				Si no es la raiz 
			 * 					me fijo si en el nivel proximo esta el proximo subdominio a agregar
			 * 						si no esta lo agrego(se agrega en el mismo nivel,antes que el null)
			 * 				encolo null
			 * 
			 * */			
			//Recorro todo el arbol,para hallar la hoja del subdominio. A esa hoja le pido la informacion que necesito
			
		public void devolverIpyServicios(string dominio){
			string[] valores= dominio.Split('.');
			Array.Reverse(valores); 
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
						c.encolar(null);}
				}
				else{
					if(nivel>=0 && nivel< valores.Length)
					{
						if(string.Compare( arbolAux.textoEtiqueta,valores[nivel]) == 0  )
						{
							siCoincideDominio=true;
							arbolAuxPadre=arbolAux;
						}
						else
							siCoincideDominio=false;
					}
					if(nivel==-1)
					{
						foreach(var hijo in arbolAux.hijos)
							c.encolar(hijo);
					}
					else
						if(siCoincideDominio){
						foreach(var hijo in arbolAux.hijos)
							c.encolar(hijo);
						}
				}
			}
			if(valores[valores.Length-1]==arbolAuxPadre.getTextoEtiquetaRaiz())
				Console.WriteLine("La dirección IP es : "+arbolAuxPadre.getDireccionIP());
			else
				Console.WriteLine("No se encuentra la dirección IP,ya que  no existe el dominio buscado");
			
		}
			/*Mientras haya subdominios a eliminar
 * 	Creo un array de Arboles Generales,en donde se va a apuntar a los arboles a eliminar
 * Se va a tener un int auxiliar que tiene el indice de aEliminar
 * Se realiza el recorrido por niveles similar al del agregado.
 * Cuando encuentre el subdominio a aeliminar lo agrego al array aEliminar y incremento aux
 * Una vez que termina el recorrido invierto el array aEliminar
 * Antes de eliminar chequeo si hay elementos a eliminar,recorriendo el array a Eliminar si hay un null significa que un subdomonio no se encontro
 * 
 * si hay para eliminar
 * Se le asigna el arbol a eliminar al primer elemento de AEliminar
 * si est ultimo no es null,le asigno el padreaeliminar al padre.
 * Si padre a eliminar no es nulo
 * 	elimino el elemento
 * mientras que el padre sea hoja    si el padre no tiene mas hijos
 * 	El arbol a eliminar ahora es el padre y el padre es el padre del padre
 * elimino el arbol a eliminar
 * si el padre  a eliminar es null llegue al finl, corto la ejecucion
 * 
 * imprimo q se elimino
 * 
 * si no hay para elkiminar lo imprimo
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * */
	public void eliminarUrl(string dominio){
	
	string[] valores= dominio.Split('.');
	Array.Reverse(valores); //Doy vuelta los valores del array para que coincidan los niveles con los indices.
	bool hayEliminar=true;  //Booleano auxiliar para ver si sigo eliminando mismos subdominios .
	bool imprimio=false;
	
	while(hayEliminar){  //Se pone todo en un bucle para chequear si hay otro subdominio con la misma ettiqueta para eliminar
		ArbolGeneral[] aEliminar= new ArbolGeneral[(valores.Length+1)];  //Array de arboles que van a apuntar los subdominios a eliminar
		aEliminar[0]=this;
		Cola c = new Cola();
		ArbolGeneral arbolAux;
		int nivel = -1;
		int aux1=0;//Auxiliar de indice de valores
		c.encolar(this);
		c.encolar(null);
		while(!c.esVacia()){
			arbolAux = c.desencolar();
			if(arbolAux == null){
				if(!c.esVacia()){
					nivel++;
					c.encolar(null);}
			}
			else{
				if(nivel>=0 && aux1< valores.Length)
				{
					if(string.Compare( arbolAux.textoEtiqueta,valores[aux1]) == 0  ) //Si encuentro el subdominio a eliminar
					{aEliminar[aux1+1]=arbolAux;//Lo agrego al array para eliminarlo
						aux1++; } //Incremento el indice de valores,para buscar el proximo subdominio a eliminar
				}
				foreach(var hijo in arbolAux.hijos) //Encolo todos los hijos,pq puede haber el caso que un usuario elimina facebook,sin poner el com o org.
				c.encolar(hijo);
			}
		}
		Array.Reverse( aEliminar); //Invierto los valores del array por implementacion
		foreach (ArbolGeneral element in aEliminar) {
			if(element==null)  //Si tengo un null en aEliminar significa que una parte no se encontro.
				hayEliminar=false;
		}
		if(hayEliminar){  //Si no hay entonces procedo a eliminar
			ArbolGeneral arbolAEliminar=aEliminar[0];  //Se le asigna el primer elemento a eliminar,luego se va a ir barriendo
			ArbolGeneral padreDeAEliminar=aEliminar[0].getPadre();
			if(padreDeAEliminar != null){
				padreDeAEliminar.eliminarHijo(arbolAEliminar);
				while(padreDeAEliminar.esHoja()){ //Un bucle para ver si mientras elimino subdominio,el padre es hoja, en tal caso lo elimino
					arbolAEliminar=padreDeAEliminar;
					padreDeAEliminar=padreDeAEliminar.getPadre();
					if(padreDeAEliminar != null)
						padreDeAEliminar.eliminarHijo(arbolAEliminar);
					if(padreDeAEliminar==null)
						break;
				}
			}
				
			if(!imprimio){
				Console.WriteLine("Se elimino correctamente la url");
				imprimio=true;
			}
		}
		else
			if(!imprimio){
			Console.WriteLine("No se encontro la Url a eliminar");
			imprimio=true;
		}
		

	}
	
}

	public void imprimirSubdominios(string subdominio)
		{

			List <ArbolGeneral> subdominios= new List<ArbolGeneral>();
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
						c.encolar(null);
					}
				}
				else{
					if(nivel>=0 )
					{
						if(string.Compare( arbolAux.textoEtiqueta,subdominio) == 0)
						{
							arbolAuxPadre=arbolAux; //Pasa a ser el padre,ya que el proximo subdominio se le asignara a este
							_imprimirSubdominios(arbolAuxPadre);  //Cada vez q encuentre el subdominio veo los subdominios que depedne de el
							
						}
						
					}
					
					if(nivel==-1)
					{
						foreach(var hijo in arbolAux.hijos) c.encolar(hijo);
					}
					else
						foreach(var hijo in arbolAux.hijos)
							c.encolar(hijo);
					
				}
			}

		}
		
		
		private void _imprimirSubdominios(ArbolGeneral arbol){
			int cantUrls= arbol.ancho();

			Pila[] Urls = new Pila [cantUrls]; //Array de Pilas,cada pila va a ir formando el dominio
			for (int i = 0; i < Urls.Length; i++) {
				//Las inicializo y ya almaceno el primer subdominio
				Urls[i]=new Pila();
				Urls[i].apilar(arbol);
			}
			int contPila=0;
			Cola c = new Cola();
			ArbolGeneral arbolAux;
			int nivel = 0;
			c.encolar(arbol);
			c.encolar(null);
			
			while(!c.esVacia()){
				arbolAux = c.desencolar();
				
				if(arbolAux == null){
					if(!c.esVacia()){
						nivel++;
						contPila=0;
						c.encolar(null);
					}
				}
				else{
					if(!arbolAux.esHoja() && nivel !=0){  //Si no es hoja lo almaceno en todos los subdominios q lo comparten,lo comparten
						int cantHijos= arbolAux.getHijos().Count;
						for (int i = 0; i < cantHijos; i++) {
							Urls[contPila].apilar(arbolAux);
							contPila++;
						}
					}
					else{
						if(nivel !=0){
							while(contPila< Urls.Length)
							{
								if(Urls[contPila].tope().getHijos().Contains(arbolAux) ){
									Urls[contPila].apilar(arbolAux);
									contPila++;
									break;
								} //Si es hoja solo lo almaceno en el indice q corresponde
								contPila++;
							}
						
//					for (int i = 0; i < Urls.Length; i++) {
//								ArbolGeneral arbolAuxx = Urls[i].tope();
//								foreach (ArbolGeneral ar in arbolAuxx.getHijos()) {
//									if(string.Compare( ar.textoEtiqueta,arbolAux.getTextoEtiquetaRaiz()) == 0){
//										Urls[i].apilar(arbolAux);
//										contPila++;
//										break;}
//								}
//						
//					}
						
							
							
					}
					
					
				}
					foreach(var hijo in arbolAux.hijos)
						c.encolar(hijo);

			}
			}
			
//			Imprimo todos los subdominios resultantes
			for (int i = 0; i < Urls.Length; i++) {
				string url="";
				while(!Urls[i].vacia()){
					ArbolGeneral aux= Urls[i].desapilar();
					string aux2= aux.getTextoEtiquetaRaiz();
					if(!Urls[i].vacia())
						url+=aux2+".";
					else
						url+=aux2;
				}
				Console.WriteLine(url);
			}
		}



		public void profundidadNodos(int profundidad){
			/*Dada una profundidad imprimir las cantidades de dominios de nivel superior,
subdominios y equipos ubicados a dicha profundidad.*/
			Cola c = new Cola();
			ArbolGeneral arbolAux;
			int cantAntes=0,cantProfundidad=0;
			
			int nivel = 0;
			
			c.encolar(this);
			c.encolar(null);
			
			Console.Write("Nivel " + nivel + ": ");
			
			while(!c.esVacia() ){
				
				if(nivel>profundidad)
					break;
				arbolAux = c.desencolar();
				
				if(arbolAux == null){
					if(!c.esVacia()){
						nivel++;
						
						Console.Write("\nNivel " + nivel + ": ");
						c.encolar(null);
					}
				}
				else{
					if(nivel==profundidad)
					{
						cantProfundidad++;
					}
					else{
						if(nivel !=0)
							cantAntes++;
					}
					
					
					foreach(var hijo in arbolAux.hijos)
						c.encolar(hijo);
				}
				
				
			}
			
			Console.WriteLine("Cantidad de dominios de nivel superior: "+cantAntes);
			Console.WriteLine("Cantidad de dominios de profundidad "+profundidad.ToString()+": "+cantProfundidad);
		}




	}
	
	
}
using System;
using System.Collections.Generic;

namespace TPF

{

	public class Cola
	{

		
		private List<ArbolGeneral> datos = new List<ArbolGeneral>();
	
		public List<ArbolGeneral> Datos{
			get{return datos;}
		}
		public void encolar(ArbolGeneral elem) {
			this.datos.Add(elem);
		}
	
		public ArbolGeneral desencolar() {
			ArbolGeneral temp = this.datos[0];
			this.datos.RemoveAt(0);
			return temp;
		}
		
		public ArbolGeneral tope() {
			return this.datos[0]; 
		}
		
		public bool esVacia() {
				return this.datos.Count == 0;
			}
		
		public bool contiene (string c){
			for(int i = 0; i<datos.Count; i++){
				if(datos[i].getTextoEtiquetaRaiz()==c)
					return true;
			}
			return false;
		}
	}
}

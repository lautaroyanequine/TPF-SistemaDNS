/*
 * Creado por SharpDevelop.
 * Usuario: USUARIO
 * Fecha: 23/10/2021
 * Hora: 04:31 p.m.
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections;
using System.Collections.Generic;

namespace TPF
{
	/// <summary>
	/// Description of Pila.
	/// </summary>
		public class Pila 
	{
		private List<ArbolGeneral> elementos = new List<ArbolGeneral>();
		
		public List<ArbolGeneral> Elementos{
			get{return elementos;}
		}
		public void apilar(ArbolGeneral elem)   
		{
			elementos.Add(elem);
		}
		public ArbolGeneral desapilar()
		{
			ArbolGeneral aux;
			int tam=elementos.Count;
			aux=(ArbolGeneral)elementos[tam-1];
			elementos.Remove(aux);
			return aux;
		}
		public bool vacia()
		{
			return elementos.Count==0;
		}
		public ArbolGeneral tope()
		{
			int tam=elementos.Count;
			return (ArbolGeneral) elementos[tam-1];
		}
	}
	}


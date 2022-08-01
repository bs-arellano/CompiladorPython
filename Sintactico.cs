//Brayan Arellano - 20191020151
//Jesus Lozada - 20191020098
//Juan Hurtado - 20191020082

using System;
using System.Collections;

//Fase de analisis sintactico
namespace Sintactico
{
    class Sintactico
    {
        ArbolSintactico arbol;
        bool correcto = true;
        public void Analizar(ArrayList tokens)
        {
            arbol = new ArbolSintactico(Tuple.Create("Codigo Python", "root"));
            //Almacena temporalmente los tokens de tipo operador y operando
            Stack<Tuple<string, string>> operadores = new Stack<Tuple<string, string>>();
            Stack<Tuple<string, string>> operandos = new Stack<Tuple<string, string>>();
            Nodo actual = arbol.getRoot();
            foreach (Tuple<string, string> t in tokens)
            {
                if (esFuncion(t.Item2))
                {
                    Nodo funcion = new Nodo(t);
                    actual.addNodo(funcion);
                }
                //Abre o cierra funcion
                else if (t.Item2 == "LBRACE")
                {
                    actual = actual.getHijos().Last();
                }
                else if (t.Item2 == "RBRACE")
                {
                    actual = actual.getPadre();
                }
                //Abre o cierra expresion
                else if (t.Item2 == "LPAR")
                {
                    if (esFuncion(actual.getHijos().Last().getValor().Item2))
                    {
                        actual = actual.getHijos().Last();
                    }

                }
                else if (t.Item2 == "RPAR")
                {
                    if (esFuncion(actual.getValor().Item2))
                    {

                        for (int i = 0; i < operandos.Count(); i++)
                        {
                            actual.addNodo(new Nodo(operandos.Pop()));
                        }
                        actual = actual.getPadre();
                    }
                }
                //Almacena operador y operandos
                else if (esOperador(t.Item2))
                {
                    operadores.Push(t);
                }
                else
                {
                    operandos.Push(t);
                    if (operandos.Count == 2 & operadores.Count == 1)
                    {
                        //Crea nodos de los tokens almacenados temporalmente
                        Nodo operador = new Nodo(operadores.Pop());
                        Nodo op2 = new Nodo(operandos.Pop());
                        Nodo op1 = new Nodo(operandos.Pop());
                        //Añade el operador y los operandos al nodo actual
                        operador.addNodo(op1);
                        operador.addNodo(op2);
                        actual.addNodo(operador);
                    }
                }
            }
        }
        //Clasifica un token dado como operador
        public bool esOperador(String token)
        {
            string[] operadores = {"AMPER", "PERCENT", "STAR", "PLUS", "MINUS", "SLASH", "DOT",
                "COLON", "LESS", "EQUAL", "GREATER", "CIRCUMFLEX", "NOTEQUAL", "DOUBLESTAR", "STAREQUAL",
                "PLUSEQUAL", "MINEQUAL", "SLASHEQUAL", "EQEQUAL", "ID518"};
            return operadores.Contains(token) ;
        }
        //Clasifica un token dado como funcion
        public bool esFuncion(String token)
        {
            string[] funciones = { "ID510", "ID530", "ID517", "ID516", "ID506", "ID51"};
            return funciones.Contains(token) ;
        }
        //Retorna el resultadfo del analisis sintactico
        public bool getCorrecto()
        {
            return correcto;
        }
        //Retorna el arbol sintactico
        public ArbolSintactico getArbol()
        {
            return arbol;
        }
    }

    class ArbolSintactico
    {
        Nodo raiz;
        //Constructor requiere un nodo raiz
        public ArbolSintactico(Tuple<string, string> root)
        {
            raiz = new Nodo(root);
        }
        //Devuelve el nodo raiz del arbol
        public Nodo getRoot()
        {
            return raiz;
        }
        //Devuelve un string con el arbol
        public async void imprimir()
        {
            String texto = raiz.printNodo("");
            await File.WriteAllTextAsync(@"d:\ArbolSintactico_Log.txt", texto);
        }
    }

    class Nodo
    {
        Tuple<string, string> valor;
        Nodo padre;
        List<Nodo> hijos = new List<Nodo>();
        //Metodo constructor
        public Nodo(Tuple<string, string> valor)
        {
            this.valor = valor;
        }
        //Nodo padre
        public void setPadre(Nodo valor)
        {
            this.padre = valor;
        }
        public Nodo getPadre()
        {
            return padre;
        } 
        //Nodos hijos
        public void addNodo(Nodo valor)
        {
            hijos.Add(valor);
            valor.setPadre(this);
        }
        public List<Nodo> getHijos()
        {
            return hijos;
        }
        //imprimir nodo
        public String printNodo(String prefijo)
        {
            String respuesta = prefijo+this.valor.Item1+"\n";
            foreach (Nodo hijo in hijos)
            {
                respuesta += hijo.printNodo(prefijo+'-');
            }
            return respuesta;
        }
        public Tuple<string, string> getValor()
        {
            return this.valor;
        }
    }
}
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
                //Limpia pilas de operaciones
                if (!esOperador(t.Item2))
                {
                    if (operandos.Count==2 && operadores.Count == 1)
                    {
                        Nodo op = new Nodo(operadores.Pop());
                        Nodo op2 = new Nodo(operandos.Pop());
                        Nodo op1 = new Nodo(operandos.Pop());
                        op.addNodo(op1);
                        op.addNodo(op2);
                        actual.addNodo(op);
                    }
                }
                //Funcion if for
                if (esFuncion(t.Item2))
                {
                    Nodo funcion = new Nodo(t);
                    actual.addNodo(funcion);
                    continue;
                }
                //Abre o cierra funcion
                if (t.Item2 == "LBRACE")
                {
                    actual = actual.getHijos().Last();
                    continue;
                }
                if (t.Item2 == "RBRACE")
                {
                    actual = actual.getPadre();
                    continue;
                }
                //Abre o cierra expresion
                if (t.Item2 == "LPAR")
                {
                    if (esFuncion(actual.getHijos().Last().getValor().Item2))
                    {
                        actual = actual.getHijos().Last();
                    }
                    continue;
                }
                if (t.Item2 == "RPAR")
                {
                    if (esFuncion(actual.getValor().Item2))
                    {

                        for (int i = 0; i < operandos.Count(); i++)
                        {
                            actual.addNodo(new Nodo(operandos.Pop()));
                        }
                        actual = actual.getPadre();
                    }
                    continue;
                }
                //Almacena operador y operandos
                if (esOperador(t.Item2))
                {
                    operadores.Push(t);
                    continue;
                }
                else
                {
                    if (operandos.Count <= 1)
                    {
                        operandos.Push(t);
                        continue;
                    }
                    if (operandos.Count == 2 && operadores.Count == 2)
                    {
                        operandos.Push(t);
                        Nodo op2 = new Nodo(operadores.Pop());
                        Nodo opn3 = new Nodo(operandos.Pop());
                        Nodo opn2 = new Nodo(operandos.Pop());
                        op2.addNodo(opn2);
                        op2.addNodo(opn3);
                        Nodo op1 = new Nodo(operadores.Pop());
                        Nodo opn1 = new Nodo(operandos.Pop());
                        op1.addNodo(opn1);
                        op1.addNodo(op2);
                        actual.addNodo(op1);
                        continue;
                    }
                }
            }
            if (operandos.Count == 2 && operadores.Count == 1)
            {
                Nodo op = new Nodo(operadores.Pop());
                Nodo op2 = new Nodo(operandos.Pop());
                Nodo op1 = new Nodo(operandos.Pop());
                op.addNodo(op1);
                op.addNodo(op2);
                actual.addNodo(op);
            }
            if (operadores.Count > 0 | operandos.Count > 0)
            {
                correcto = false;
            }
            else
            {
                correcto = true;
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
using Sintactico;
using System.Collections;

namespace Semantico
{
    class Semantico
    {
        public bool AnalizarTipos(ArrayList tokens, ArbolSintactico arbol)
        {
            bool correcto = true;
            //Asigna un tipo a cada variable
            ArrayList tipos = new ArrayList();
            foreach(Tuple<string, string> t in tokens)
            {
                if (t.Item2 == "Variable")
                {
                    if (t.Item1[0] == '"' & t.Item2[t.Item2.Length-1]=='"')
                    {
                        tipos.Add(Tuple.Create(t.Item1,"String"));
                    }
                    else if (t.Item1 == "True" | t.Item1 == "False")
                    {
                        tipos.Add(Tuple.Create(t.Item1, "Boolean"));
                    }
                    else
                    {
                        //Si contiene un unico punto es potencialmente un flotante
                        bool flotante = false;
                        if (t.Item1.Count(f => (f == '.')) > 1)
                        {
                            flotante = true;
                        }
                        //Revisa si unicamente contiene caracteres numericos
                        bool entero = true;
                        foreach(char c in t.Item1)
                        {
                            if (c < 48 | c > 57)
                            {
                                if (c=='.'&flotante)
                                {
                                    entero = true;
                                }
                                else
                                {
                                    entero = false;
                                }
                            }
                        }
                        if (entero)
                        {
                            if (flotante)
                            {
                                tipos.Add(Tuple.Create(t.Item1, "Float"));
                            }
                            else
                            {
                                tipos.Add(Tuple.Create(t.Item1, "Integer"));
                            }
                        }
                        //No es ningun tipo primitivo
                        else
                        {
                            tipos.Add(Tuple.Create(t.Item1, "Variable"));
                        }
                    }
                }
            }
            //Recorre el arbol recursivamente
            
            return correcto;
        }
    }
}

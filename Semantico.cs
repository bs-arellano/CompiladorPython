using Sintactico;
using System.Collections;

namespace Semantico
{
    class Semantico
    {
        //Lista de los tokens y sus tipos
        ArrayList tipos = new ArrayList();
        public bool AnalizarTipos(ArrayList tokens, ArbolSintactico arbol)
        {
            //Asigna un tipo a cada variable
            foreach(Tuple<string, string> t in tokens)
            {
                if (t.Item2 == "Variable")
                {
                    if (t.Item1[0] == '"' & t.Item1[t.Item1.Length-1]=='"')
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
            return compConsistencia(arbol.getRoot()); ;
        }
        public async void imprimirTipos()
        {
            String texto = "";
            foreach(Tuple<String,String> tupla in tipos)
            {
                texto += tupla.Item1 + " " + tupla.Item2 + "\n";
            }
            await File.WriteAllTextAsync(@"d:\TablaSemantica.txt", texto);
        }
        bool compConsistencia(Nodo nodo)
        {
            //Si el nodo actual es un operador
            if (nodo.getValor().Item1 == "=")
            {
                String tipo = "";
                //Revisa los nodos hijos del operador
                foreach(Nodo hijo in nodo.getHijos())
                {
                    //Busca el token en la lista de tipos
                    for (int i = 0; i < tipos.Count; i++)
                    {
                        //Recupera la tupla en la posicion i
                        Tuple<String, String> tupla = (Tuple < String, String>) tipos[i];
                        if (tupla.Item1 == hijo.getValor().Item1)
                        {
                            //Si no hay un tipo asiganado
                            if (tipo == "")
                            {
                                if (tupla.Item2 != "Variable")
                                {
                                    tipo = tupla.Item2;
                                }
                            }
                            //Si no coinciden los tipos retorna falso a menos que sea una variable sin tipo
                            else if (tipo != tupla.Item2)
                            {
                                if (tupla.Item2 == "Variable")
                                {
                                    tupla = Tuple.Create("Hola", "Que tal");
                                }
                                else
                                {
                                    return false;
                                    break;
                                }
                            }
                        }
                    }
                }
                return true;
            }
            //Si no es operador comprueba consistencia de los hijos
            else
            {
                bool consistente=true;
                foreach(Nodo hijo in nodo.getHijos())
                {
                    consistente = compConsistencia(hijo);
                    if (!consistente)
                    {
                        return false;
                        break;
                    }
                }
                return consistente;
            }
        }
    }
}

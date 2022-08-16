using System.Diagnostics;

namespace Intermedio
{
    class Intermedio
    {
        int n = 0;
        string texto = "";
        Dictionary<string, string> variables = new Dictionary<string, string>();
        public void generar(Sintactico.ArbolSintactico arbol)
        {
            leerOperacion(arbol.getRoot());
            File.WriteAllText(@"A:\CodigoIntermedio.txt", texto + '\n' + "DICCIONARIO DE VARIABLES: \n");
            foreach (string key in variables.Keys) 
            {
                File.AppendAllText(@"A:\CodigoIntermedio.txt", variables[key] + " : " + key + '\n');
            }

        }
        string RevisarDiccionario(string t)
        {
            if (variables.TryGetValue(t, out string var)){
                return var;
            }
            else
            {
                var = String.Concat("var", n);
                n++;
                variables.Add(t, var);
                return var;
            }
        }
        void leerOperacion(Sintactico.Nodo nodo)
        {
            //Nodo actual es operador
            if (esOperador(nodo.getValor().Item1)){
                //Si el operando derecho es una operacion la lee primero
                if (esOperador(nodo.getHijos().Last().getValor().Item1)){
                    texto += RevisarDiccionario(nodo.getHijos().First().getValor().Item1);
                    texto += nodo.getValor().Item1;
                    texto += RevisarDiccionario(nodo.getHijos().Last().getHijos().First().getValor().Item1);
                    texto += nodo.getHijos().Last().getValor().Item1;
                    texto += RevisarDiccionario(nodo.getHijos().Last().getHijos().Last().getValor().Item1);
                    texto += "\n";
                }
                else
                {
                    texto += RevisarDiccionario(nodo.getHijos().First().getValor().Item1);
                    texto += nodo.getValor().Item1;
                    texto += RevisarDiccionario(nodo.getHijos().Last().getValor().Item1);
                    texto += "\n";
                }
            }
            //Si no busca en los hijos
            else
            {
                foreach (Sintactico.Nodo hijo in nodo.getHijos()) {
                    leerOperacion(hijo);
                }
            }
        }
        bool esOperador(string t)
        {
            string[] ops = { "+", "-", "*", "/", "=" };
            return ops.Contains(t);
        }
    }
}

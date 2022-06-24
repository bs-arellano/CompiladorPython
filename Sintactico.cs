//Brayan Arellano - 20191020151
//Jesus Lozada - 20191020098
//Juan Hurtado - 20191020082

using System;
using System.Collections;

namespace Sintactico
{
    class Sintactico
    {
        bool correcto = true;
        public void Analizar(ArrayList t)
        {
            ArrayList tokens = t;
            for(int i = 0; i<tokens.Count-1; i++)
            {
                if(((Tuple<string, string>) tokens[i]).Item2 == "ID510")
                {
                    if(((Tuple<string, string>) tokens[i+1]).Item2 != "Variable")
                    {
                        correcto = false;
                    }

                    if(((Tuple<string, string>) tokens[i+3]).Item2 != "Variable")
                    {
                        correcto = false;
                    }

                    if(((Tuple<string, string>) tokens[i+4]).Item2 != "COLON")
                    {
                        correcto = false;
                    }
                    if(((Tuple<string, string>) tokens[i+2]).Item2 == "EQEQUAL"
                        || ((Tuple<string, string>) tokens[i+2]).Item2 == "LESS"
                        || ((Tuple<string, string>) tokens[i+2]).Item2 == "GREATER"
                        || ((Tuple<string, string>) tokens[i+2]).Item2 == "NOTEQUAL"
                        || ((Tuple<string, string>) tokens[i+2]).Item2 == "LESSEQUAL"
                        || ((Tuple<string, string>) tokens[i+2]).Item2 == "GREATEREQUAL")
                    {} else {
                        correcto = false;
                    }  
                }
            }
        }
        public bool getCorrecto()
        {
            return correcto;
        }
    }
}
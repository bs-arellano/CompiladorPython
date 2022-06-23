//Brayan Arellano - 20191020151
//Jesus Lozada - 20191020098
//Juan Hurtado - 20191020082
using System;
using System.Collections;

namespace Lexico
{
    class main
    {
        static void Main(String[] args)
        {
            //Instancia de la clase Identificador
            Identificador identificador = new Identificador();
            Tokenizer tokenizer = new Tokenizer();
            //Lee el codigo fuente
            Console.WriteLine("*******************");
            string codigo = File.ReadAllText("test.py");
            Console.WriteLine(codigo);
            Console.WriteLine("*******************");
            //Lista de tokens identificados
            ArrayList tokens = new ArrayList();
            string word = "";
            string ultimo = "";
            char anterior = ' ';
            foreach (char c in codigo)
            {
                //Texto en comillas
                if (tokens.Count >= 1)
                {
                    ultimo = tokens[tokens.Count - 1].ToString();
                    if (ultimo[0]=='"' && (ultimo.Length>1 ^ ultimo[ultimo.Length - 1] == '"'))
                    {
                        ultimo += c;
                        tokens.RemoveAt(tokens.Count - 1);
                        tokens.Add(ultimo);
                        continue;
                    }
                }
                //Es una palabra o numero
                if (identificador.esLetra(c) | identificador.esNumero(c))
                {
                    word += c;
                }
                else if (word.Length > 0)
                {
                    tokens.Add(word);
                    word = "";
                }
                //Es un simbolo
                if (identificador.esSimbolo(c))
                {
                    //Parentesis
                    if (c=='(' || c == ')')
                    {
                        tokens.Add(c.ToString());
                        anterior = ' ';
                        continue;
                    }
                    //Operador de multiple simbolo
                    else if (identificador.esSimbolo(anterior))
                    {
                        ultimo += c;
                        tokens.RemoveAt(tokens.Count - 1);
                        tokens.Add(ultimo);
                    }
                    //Operador simple
                    else
                    {
                        tokens.Add(c.ToString());
                    }
                }
                anterior = c;
            }
            //Imprime lista de tokens identificados
            foreach (string t in tokens)
            {
                Console.WriteLine(t+"   "+tokenizer.getToken(t));
            }
            Console.ReadKey();
        }
    }
    class Identificador
    {
        public Boolean esLetra(char c)
        {
            //Es una letra
            if (c >= 65 && c <= 90)
            {
                return true;
            }
            else if (c >= 97 && c <= 122)
            {
                return true;
            }
            else return false;
        }
        public Boolean esNumero(char c)
        {
            //Es un numero
            if (c >= 48 && c <= 57)
            {
                return true;
            }
            else return false;
        }
        public Boolean esSimbolo(char c)
        {
            //Es un simbolo o operador
            if (c >= 33 && c <= 47)
            {
                return true;
            }
            else if (c >= 58 && c <= 64)
            {
                return true;
            }
            else if (c >= 91 && c <= 95)
            {
                return true;
            }
            else return false;
        }
    }
    class Tokenizer
    {
        Dictionary<string, string> tokens = new Dictionary<string, string>()
        {
            //Keywords
            {"if","ID510"},{"in","ID518"},{"as","ID520"},{"is","ID530"},{"or","ID531"},
            {"del","ID503"},{"try","ID511"},{"for","ID517"},{"def","ID526"},{"not","ID529"},{"and","ID532"},
            {"pass","ID502"},{"from","ID514"},{"elif","ID515"},{"else","ID516"},{"with","ID519"},{"None","ID523"},{"True","ID524"},
            {"raise","ID501"},{"yield","ID504"},{"break","ID506"},{"while","ID512"},{"False","ID525"},{"class","ID527"},{"print","ID51"},{"range","ID52"},
            {"return","ID500"},{"assert","ID505"},{"global","ID508"},{"import","ID513"},{"except","ID521"},{"lambda","ID528"},
            {"finally","ID522"},{"continue","ID507"},{"nonlocal","ID509"},
            //Symbols
            {"%","PERCENT"},{"&","AMPER"},{"(","LPAR"},{")","RPAR"},{"*","STAR"},{"+","PLUS"},
            {",","COMMA"},{"-","MINUS"},{".","DOT"},{"/","SLASH"},{":","COLON"},{";","SEMI"},
            {"<","LESS"},{"=","EQUAL"},{">","GREATER"},{"@","AT"},{"[","LSQB"},{"]","RSQB"},
            {"^","CIRCUMFLEX"},{"{","LBRACE"},{"|","VBAR"},{"}","RBRACE"},{"~","TILDE"},
            //Double symbols
            {"!=","NOTEQUAL"},{"%=","PERCENTEQUAL"},{"&=","AMPEREQUAL"},{"**","DOUBLESTAR"},
            {"*=","STAREQUAL"},{"+=","PLUSEQUAL"},{"-=","MINEQUAL"},{"->","RARROW"},{"//","DOUBLESLASH"},
            {"/=","SLASHEQUAL"},{":=","COLONEQUAL"},{"<<","LEFTSHIFT"},{"<=","LESSEQUAL"},{"<>","NOTEQUAL"},
            {"==","EQEQUAL"},{">=","GREATEREQUAL"},{">>","RIGHTSIFT"},{"@=","ATEQUAL"},{"^=","CIRCUMFLEXEQUAL"},
            {"|=","VBAREQUAL"}
        };
        //Consulta la key en el diccionario y devuelve el token correspondiente
        public string getToken(string t)
        {
            tokens.TryGetValue(t, out string s);
            if (s==null)
            {   
                s = "Variable";
            }
            return s;
        }
    }
}
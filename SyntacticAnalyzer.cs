using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            LexicalAnalyzer ana = new LexicalAnalyzer();
            ana.ReadCodes();
            ana.Analyse();
            ArrayList a = ana.Words;
            Console.WriteLine("**************************\nOUTPUT:\n**************************\n");
            foreach(word w in a)
            {
                Console.WriteLine(w.ToString());
            }
            
       
            Console.Read();
            Console.Read();
        }
    }
    
    //Save a word in the struct
    public struct word
    {
        public int type;
        public string content;
        public override string ToString()
        {
            return "(" + type + ",'" + content + "')";
        }
    }

    //lexical analyzer
    class LexicalAnalyzer
    {
        private string codes;//save codes
        private ArrayList words;//save all words

        public ArrayList Words
        {
            get
            {
                return words;
            }
        }

        public LexicalAnalyzer() { }//LexicalAnalyzer with an anonymous constructor
        //Save code string into 'codes'
        public void ReadCodes()
        {
            Console.WriteLine("Please write your codes terminate with '~'.");
            codes = "";
            int codeTemp;
            while((codeTemp=Console.Read())!='~')
            {
                codes += Convert.ToChar(codeTemp);
            }
            codes += '\0';
        }

        public void Analyse()
        {
            string input;//a buffer to temporaily save a string word 
            int p_input;//pointer to the buffer position
            int p_code = 0; //pointer to the code string
            char tempChar;

            words = new ArrayList();//Initialize words
            word tempWord = new word();
            while (p_code < codes.Length)
            {
                overSpaces(ref p_code);

                tempWord.type = 10;
                tempWord.content = "";
                tempChar = codes[p_code];
                input = "";
                p_input = 0;

                if (isLetter(tempChar))
                {
                    while ((isLetter(tempChar) || isDigit(tempChar)) && p_code<codes.Length)
                    {
                        input += tempChar;
                        p_input++;
                        p_code++;
                        if (p_code < codes.Length)
                            tempChar = codes[p_code];
                    }
                    tempWord.content = input;
                    words.Add(tempWord);
                }
                else if (isDigit(tempChar))
                {
                    while (isDigit(tempChar) && p_code < codes.Length)
                    {
                        input += tempChar;
                        p_input++;
                        p_code++;
                        tempChar = codes[p_code];
                    }
                    tempWord.content = input;
                    tempWord.type = 20;
                    words.Add(tempWord);
                }
                else
                {
                    tempChar = codes[p_code];
                    switch (tempChar)
                    {
                        case '=':
                            if (++p_code < codes.Length)
                            {
                                tempChar = codes[p_code];
                                if (tempChar == '=')
                                {
                                    p_code++;
                                    tempWord.type = 39;
                                    tempWord.content = "==";
                                    words.Add(tempWord);
                                }
                                else
                                {
                                    tempWord.type = 21;
                                    tempWord.content = "=";
                                    words.Add(tempWord);
                                }
                            }
                            else
                            {
                                tempWord.type = 21;
                                tempWord.content = "=";
                                words.Add(tempWord);
                            }
                            break;
                        case '+':
                            p_code++;
                            tempWord.type = 22;
                            tempWord.content = "+";
                            words.Add(tempWord);
                            break;
                        case '-':
                            p_code++;
                            tempWord.type = 23;
                            tempWord.content = "-";
                            words.Add(tempWord);
                            break;
                        case '*':
                            p_code++;
                            tempWord.type = 24;
                            tempWord.content = "*";
                            words.Add(tempWord);
                            break;
                        case '/':
                            p_code++;
                            tempWord.type = 25;
                            tempWord.content = "/";
                            words.Add(tempWord);
                            break;
                        case '(':
                            p_code++;
                            tempWord.type = 26;
                            tempWord.content = "(";
                            words.Add(tempWord);
                            break;
                        case ')':
                            p_code++;
                            tempWord.type = 27;
                            tempWord.content = ")";
                            words.Add(tempWord);
                            break;
                        case '[':
                            p_code++;
                            tempWord.type = 28;
                            tempWord.content = "[";
                            words.Add(tempWord);
                            break;
                        case ']':
                            p_code++;
                            tempWord.type = 29;
                            tempWord.content = "]";
                            words.Add(tempWord);
                            break;
                        case '{':
                            p_code++;
                            tempWord.type = 30;
                            tempWord.content = "{";
                            words.Add(tempWord);
                            break;
                        case '}':
                            p_code++;
                            tempWord.type = 31;
                            tempWord.content = "}";
                            words.Add(tempWord);
                            break;
                        case ',':
                            p_code++;
                            tempWord.type = 32;
                            tempWord.content = ",";
                            words.Add(tempWord);
                            break;
                        case ':':
                            p_code++;
                            tempWord.type = 33;
                            tempWord.content = ":";
                            words.Add(tempWord);
                            break;
                        case ';':
                            p_code++;
                            tempWord.type = 34;
                            tempWord.content = ";";
                            words.Add(tempWord);
                            break;
                        case '>':
                            if (++p_code < codes.Length)
                            {
                                tempChar = codes[p_code];
                                if (tempChar == '=')
                                {
                                    p_code++;
                                    tempWord.type = 37;
                                    tempWord.content = ">=";
                                    words.Add(tempWord);
                                }
                                else
                                {
                                    tempWord.type = 35;
                                    tempWord.content = ">";
                                    words.Add(tempWord);
                                }
                            }
                            else
                            {
                                tempWord.type = 35;
                                tempWord.content = ">";
                                words.Add(tempWord);
                            }
                            break;
                        case '<':
                            if (++p_code < codes.Length)
                            {
                                tempChar = codes[p_code];
                                if (tempChar == '=')
                                {
                                    p_code++;
                                    tempWord.type = 38;
                                    tempWord.content = "<=";
                                    words.Add(tempWord);
                                }
                                else
                                {
                                    tempWord.type = 36;
                                    tempWord.content = "<";
                                    words.Add(tempWord);
                                }
                            }
                            else
                            {
                                tempWord.type = 36;
                                tempWord.content = "<";
                                words.Add(tempWord);
                            }
                            break;
                        case '!':
                            if (++p_code < codes.Length)
                            {
                                tempChar = codes[p_code];
                                if (tempChar == '=')
                                {
                                    p_code++;
                                    tempWord.type = 40;
                                    tempWord.content = "!=";
                                    words.Add(tempWord);
                                }
                                else
                                {
                                    tempWord.type = -1;
                                    tempWord.content = "ERROR";
                                    words.Add(tempWord);
                                }
                            }
                            else
                            {
                                tempWord.type = -1;
                                tempWord.content = "ERROR";
                                words.Add(tempWord);
                            }
                            break;
                        case '\0':
                            p_code++;
                            tempWord.type = 1000;
                            tempWord.content = "OVER";
                            words.Add(tempWord);
                            break;
                        case ' ':
                            p_code++;
                            break;
                        case '\r':
                            p_code++;
                            break;
                        case '\t':
                            p_code++;
                            break;
                        default:
                            p_code++;
                            tempWord.type = -1;
                            tempWord.content = "ERROR";
                            words.Add(tempWord);
                            break;
                    }
                }                
            }

        }
        //jump over space
        public void overSpaces(ref int p_code)
        {

            while (codes[p_code] == ' ' || codes[p_code] == Convert.ToChar(10))
            {
                p_code++;
                if (p_code == codes.Length)
                {
                    p_code--;
                    return;
                }
            }
        }

        public bool isLetter(char ch)
        {
            if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z'))
                return true;
            else
                return false;
        }

        public bool isDigit(char ch)
        {
            if (ch >= '0' && ch <= '9')
                return true;
            else
                return false;
        }

        public void retrace(ref int p_code)
        {
            p_code--;
        }
    }
}

//syntactic analyzer
class SyntacticAnalyzer
{
    ArrayList list;
    private int index = 0;

    public SyntacticAnalyzer(ArrayList a)
    {
        list = a;
    }

    public void Analyze()
    {
        Parser();
    }

    private void Parser()
    {
        if (GetContent(index).ToUpper().Equals("BEGIN"))
        {
            index++;
            Yucu();
            if (GetContent(index).ToUpper().Equals("END"))
                Console.WriteLine("SUCCESS!");          
        }
        else
            Console.WriteLine("ERROR:there should be a \'BEGIN\'.");
        return;
    }

    private void Yucu()
    {
        Statement();
        while (GetType(index) == 34)
        {
            index++;
            Statement();
        }
        return;
    }

    private void Statement()
    {
        if (GetType(index) == 10)
        {
            index++;
            if (GetType(index) == 21)
            {
                index++;
                Expression();
            }
            else
            {
                Console.WriteLine("ERROR:there should be a := near \'" + GetContent(index) + "\'");
            }
        }
        else
            Console.WriteLine("ERROR:there shoule be an identifier near \'" + GetContent(index) + "\'");
        return;
    }

    private void Expression()
    {
        Term();
        while (GetType(index) == 22 || GetType(index) == 23)
        {
            index++;
            Term();
        }
    }

    private void Term()
    {
        Factor();
        while (GetType(index) == 24 || GetType(index) == 25)
        {
            index++;
            Factor();
        }
        return;
    }

    private void Factor()
    {
        if (GetType(index) == 20 || GetType(index) == 10)
        {
            index++;
        }
        else if (GetType(index)==26)
        {
            index++;
            Expression();
            if (GetType(index) == 27)
                index++;
            else
            {
                Console.WriteLine("ERROR:there should be a \')\' near " + GetContent(index));
                index++;
            }
        }
        else
        {
            PrintError(GetContent(index));
        }
        return;
    }

    private int GetType(int i)
    {
        return ((ConsoleApplication3.word)list[i]).type;
    }

    private string GetContent(int i)
    {
        if (((ConsoleApplication3.word)list[i]).content.Trim().Equals("ERROR"))
            return "ERROR:character not in the character set!";
        return ((ConsoleApplication3.word)list[i]).content;
    }

    private void PrintError(string str)
    {
        Console.WriteLine("ERROR:NEAR \'" + str + "\' .");
    }
}
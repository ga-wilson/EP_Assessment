using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Enclara_Pharmacia_Assessment
{
    public partial class EP_Assessment : Form
    {
        public EP_Assessment()
        {
            InitializeComponent();
        }

        List<string> words = new List<string>();
        List<string> unique_words = new List<string>();
        List<int> count_unique = new List<int>();
        List<string> sentences = new List<string>();


        private void button1_Click(object sender, EventArgs e)
        {
            if (input_textBox1.Text == "")
            { MessageBox.Show("Please enter some kind of text."); }
            else
            {
                string input_w = input_textBox1.Text.ToLower();
                string input_s = input_textBox1.Text.ToLower();
                int NumPalindromeWords = 0;
                int NumPalindromeSentences = 0;
                unique_words.Clear();
                count_unique.Clear();

                //Compile Word List
                string word = Remove.Punctuation(input_w);
                word = Remove.SentenceEnd(word);
                words = Remove.Space_Store(word, words);


                //Compile Word List
                string sentence = Remove.Punctuation(input_s);
                sentence = Remove.Space(sentence);
                sentences = Remove.Sentence_Store(sentence, sentences);


                //Count the palindromes
                for (int i = 0; i < words.Count; i++)
                {
                    if (words[i] == Reverse.String(words[i]) && words[i] != "")
                    {
                        NumPalindromeWords++;
                    }
                }
                for (int i = 0; i < sentences.Count; i++)
                {
                    if (sentences[i] == Reverse.String(sentences[i]) && sentences[i] != "")
                    {
                        NumPalindromeSentences++;
                    }
                }


                //Count the unique words
                for (int i = 0; i < words.Count; i++)
                {
                    if (unique_words.Contains(words[i]))
                    {
                        count_unique[unique_words.IndexOf(words[i])] = count_unique[unique_words.IndexOf(words[i])] + 1;
                    }
                    else
                    {
                        unique_words.Add(words[i]);
                        count_unique.Add(1);
                    }
                }

                //Display
                pwn_label5.Visible = true;
                psn_label7.Visible = true;
                pwn_label5.Text = NumPalindromeWords.ToString();
                psn_label7.Text = NumPalindromeSentences.ToString();
                string uniques = "";
                for (int i = 0; i < unique_words.Count; i++)
                {
                    uniques = uniques + unique_words[i] + " [" + count_unique[i].ToString() + " time(s)]" + Environment.NewLine;
                }
                unique_textBox4.Text = uniques;
            }
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            string search = (letter_textBox2.Text).ToLower();
            if (search.Length < 2)
            {
                string has_letter = "";
                Boolean exist;
                for (int i = 0; i < unique_words.Count; i++)
                {
                    exist = false;
                    foreach (char letter in unique_words[i])
                    {
                        if (search == letter.ToString())
                        {
                            exist = true;
                        }
                    }
                    if (exist)
                    {
                        has_letter = has_letter + unique_words[i] + Environment.NewLine;
                    }
                }
                if (has_letter != "")
                {
                    contain_textBox3.Text = has_letter;
                }
                else
                {
                    contain_textBox3.Text = "This character does not appear in your text.";
                }
            }
            else
            {
                contain_textBox3.Text = "Please select only 1 character.";
            }
        }
        private void Input_textBox1_TextChanged(object sender, EventArgs e)
        {
            
            pwn_label5.Text = "";
            psn_label7.Text = "";
            unique_textBox4.Text = "";
            if (input_textBox1.Text == "" || letter_textBox2.Text == "")
            { search_button2.Enabled = false; }
            else
            { search_button2.Enabled = true; }
        }
        private void Letter_textBox2_TextChanged(object sender, EventArgs e)
        {
            contain_textBox3.Text = "";
            if (input_textBox1.Text == "" || letter_textBox2.Text == "")
            { search_button2.Enabled = false; }
            else
            { search_button2.Enabled = true; }
        }

        private void EP_Assessment_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Thank you so much for looking at my project and for your consideration!");
        }
    }

    public static class Remove
    {
        public static string Space(string input)
        {
            foreach (char space_char in input)
            {
                string space = space_char.ToString();
                if (space == " ")
                {
                    input = input.Remove(input.IndexOf(space), 1);
                }
            }
            return input;
        }
        public static string SentenceEnd(string input)
        {
            foreach (char punct_char in input)
            {
                string punct = punct_char.ToString();
                if (punct == "." || punct == "!" || punct == "?")
                {
                    input = input.Remove(input.IndexOf(punct), 1);
                }
            }
            return input;
        }
        public static List<string> Space_Store(string input, List<string> store)
        {
            store.Clear();
            foreach (char space_char in input)
            {
                string space = space_char.ToString();
                if (space == " ")
                {
                    store.Add(input.Substring(0, input.IndexOf(space)));
                    //Add a catch for if the +1 is out of range
                    input = input.Substring(input.IndexOf(space) + 1);
                }
            }
            store.Add(input);
            return store;
        }
        public static List<string> Sentence_Store(string input, List<string> store)
        {
            store.Clear();
            foreach (char punct_char in input)
            {
                string punct = punct_char.ToString();
                if (punct == "." || punct == "!" || punct == "?")
                {
                    store.Add(input.Substring(0, input.IndexOf(punct)));
                    //Add a catch for if the +1 is out of range
                    input = input.Substring(input.IndexOf(punct) + 1);
                }
            }
            store.Add(input);
            return store;
        }
        public static string Punctuation(string input)
        {
            foreach (char punctuation in input)
            {
                string punct = punctuation.ToString();
                if (punct == "," || punct == ";" || punct == ":" || punct == "'" || punct == "\"" || punct == "%" || punct == "$" || punct == "#" || punct == "&" || punct == "(" || punct == ")" || punct == "[" || punct == "]" || punct == "@" || punct == "^" || punct == "*" || punct == "_" || punct == "-" || punct == "+" || punct == "=" || punct == "/" || punct == "<" || punct == ">" || punct == "|" || punct == "\\" || punct == "`" || punct == "~")

                {
                    input = input.Remove(input.IndexOf(punct), 1);
                }
            }
            return input;
        }
    }

    public static class Reverse
    {
        public static string String(string initial)
        {
            string final = "";
            foreach (char letter in initial)
            {
                final = letter.ToString() + final;
            }
            return final;
        }
    }
}

//        //Give the number of palindrome words
//        //Done :)
//        //Give the number of palindrome sentences
//        //Done :)
//        //List the unique words of a paragraph with the count of the word instance
//        //Done :)
//        //Let the user also input a letter at some point and list all words containing that letter
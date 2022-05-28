using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;

namespace Lekcja3
{
    public partial class Form1 : Form
    {
        private FirebaseAuthProvider prov;
        private FirebaseAuthLink auth;
        private FirebaseClient firebase;
        private User user;
        public Form1()
        {
            InitializeComponent();
            prov = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyB6OLtr2nTsCE9QbjjzFh5ipQcogXQSscY"));
            //prov.SignInWithEmailAndPasswordAsync("jakubkaczma5@gmail.com", "Asaasa12");
            try{
                auth = prov.SignInWithEmailAndPasswordAsync("jakubkaczma5@gmail.com", "Asaasa12").GetAwaiter().GetResult();
            }
            catch (FirebaseAuthException e)
            {
                var create = prov.CreateUserWithEmailAndPasswordAsync("jakubkaczma5@gmail.com", "Asaasa12").GetAwaiter().GetResult();
                auth = prov.SignInWithEmailAndPasswordAsync("jakubkaczma5@gmail.com", "Asaasa12").GetAwaiter().GetResult();
            }
            firebase = new FirebaseClient("https://testproject-639b1-default-rtdb.europe-west1.firebasedatabase.app/",new FirebaseOptions{AuthTokenAsyncFactory = () => Task.FromResult(auth.FirebaseToken)});
            if (firebase != null)
            {
                List<string> fromCloud = firebase.Child("lists").Child(auth.User.LocalId).OnceSingleAsync<List<string>>().GetAwaiter().GetResult();
                if (fromCloud==null)
                {
                    firebase.Child("lists").Child(auth.User.LocalId).PutAsync(lines).GetAwaiter().GetResult();
                }
                else
                {
                    lines = fromCloud;
                    updateLines();
                };
            }
        }

        private List<string> lines = new List<string>();
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Return && !string.IsNullOrEmpty(textBox1.Text))
            {
                lines.Add(textBox1.Text);
                updateLines();
                textBox1.Text = "";
            }
        }
        private void updateLines()
        {
            textBox2.Lines = lines.ToArray();
            Debug.WriteLine(textBox2.Lines);
            firebase.Child("lists").Child(auth.User.LocalId).PutAsync(lines).GetAwaiter().GetResult();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}

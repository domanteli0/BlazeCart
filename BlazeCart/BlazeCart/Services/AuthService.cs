using System.Text.Json;
using Firebase.Auth;

namespace BlazeCart.Services
{
    public class AuthService
    {
        //HERE SHOULD BE API KEY
        private const string apiKey = "PASTE_KEY_HERE";
        private readonly FirebaseAuthProvider authProvider = new(new FirebaseConfig(apiKey));

        private FirebaseAuth auth = null;

        public async Task RegisterAsync(string email, string password)
        {
            try
            {
                auth = await authProvider.CreateUserWithEmailAndPasswordAsync(email, password);
            }
            catch {
                throw;
            }
        }

        public async Task LoginAsync(string email, string password)
        {
            try
            {
                auth = await authProvider.SignInWithEmailAndPasswordAsync(email, password);
            }
            catch {
                throw;
            }
        }
    }
}

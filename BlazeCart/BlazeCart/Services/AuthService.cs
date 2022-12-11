using BlazeCart.Models;
using Firebase.Auth;
using Microsoft.Extensions.Configuration;



namespace BlazeCart.Services
{
    
    public class AuthService
    {
        public AuthService(string apiKey) {
            this._authProvider = new(new FirebaseConfig(apiKey));
        }

        private readonly FirebaseAuthProvider _authProvider;
        private FirebaseAuth auth = null;

        public async Task RegisterAsync(string email, string password)
        {
            try
            {
                auth = await _authProvider.CreateUserWithEmailAndPasswordAsync(email, password);
            }
            catch {
                throw;
            }
        }

        public async Task LoginAsync(string email, string password)
        {
            try
            {
                auth = await _authProvider.SignInWithEmailAndPasswordAsync(email, password);
            }
            catch {
                throw;
            }
        }
    }
}

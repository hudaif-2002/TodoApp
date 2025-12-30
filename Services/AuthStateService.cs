namespace TodoApp.Services;

public class AuthStateService
{
    private bool _isAuthenticated = false;
    private string? _userEmail;
    
    public event Action? OnChange;

    public bool IsAuthenticated => _isAuthenticated;
    public string? UserEmail => _userEmail;

    public void SetAuthenticated(string email, string token)
    {
        _isAuthenticated = true;
        _userEmail = email;
        NotifyStateChanged();
    }

    public void Logout()
    {
        _isAuthenticated = false;
        _userEmail = null;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}

using System.ComponentModel;
using Microsoft.Extensions.Logging;

namespace NoteApp.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged, IDisposable
    {
        private bool _isBusy;
        private string _title = string.Empty;
        private string _errorMessage = string.Empty;
        private bool _hasError;
        private bool _disposed;

        protected readonly ILogger? Logger;

        protected BaseViewModel(ILogger? logger = null)
        {
            Logger = logger;
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsNotBusy));
                }
            }
        }

        public bool IsNotBusy => !IsBusy;

        public string Title
        {
            get => _title;
            set
            {
                if (_title != value)
                {
                    _title = value ?? string.Empty;
                    OnPropertyChanged();
                }
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                if (_errorMessage != value)
                {
                    _errorMessage = value ?? string.Empty;
                    OnPropertyChanged();
                    HasError = !string.IsNullOrWhiteSpace(_errorMessage);
                }
            }
        }

        public bool HasError
        {
            get => _hasError;
            private set
            {
                if (_hasError != value)
                {
                    _hasError = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Simplified async execution without complex command wrapping
        protected virtual async Task ExecuteAsync(Func<Task> operation, [System.Runtime.CompilerServices.CallerMemberName] string? operationName = null)
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                ClearError();
                
                Logger?.LogDebug("Starting operation: {OperationName}", operationName);
                await operation();
                Logger?.LogDebug("Completed operation: {OperationName}", operationName);
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "Error in operation: {OperationName}", operationName);
                HandleError(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected virtual async Task<T?> ExecuteAsync<T>(Func<Task<T>> operation, [System.Runtime.CompilerServices.CallerMemberName] string? operationName = null)
        {
            if (IsBusy)
                return default;

            try
            {
                IsBusy = true;
                ClearError();
                
                Logger?.LogDebug("Starting operation: {OperationName}", operationName);
                var result = await operation();
                Logger?.LogDebug("Completed operation: {OperationName}", operationName);
                return result;
            }
            catch (Exception ex)
            {
                Logger?.LogError(ex, "Error in operation: {OperationName}", operationName);
                HandleError(ex);
                return default;
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected virtual void HandleError(Exception exception)
        {
            ErrorMessage = exception switch
            {
                ArgumentException => "Invalid input provided.",
                InvalidOperationException => "Operation cannot be completed at this time.",
                UnauthorizedAccessException => "Access denied.",
                TimeoutException => "Operation timed out. Please try again.",
                _ => "An unexpected error occurred. Please try again."
            };
        }

        protected virtual void ClearError()
        {
            ErrorMessage = string.Empty;
        }

        // Simplified command creation methods
        protected Command CreateCommand(Action execute, Func<bool>? canExecute = null)
        {
            return new Command(execute, canExecute ?? (() => true));
        }

        protected Command CreateAsyncCommand(Func<Task> execute, Func<bool>? canExecute = null)
        {
            return new Command(async () =>
            {
                try
                {
                    await execute();
                }
                catch (Exception ex)
                {
                    Logger?.LogError(ex, "Error in async command");
                    HandleError(ex);
                }
            }, canExecute ?? (() => true));
        }

        protected Command<T> CreateCommand<T>(Action<T> execute, Func<T, bool>? canExecute = null)
        {
            return new Command<T>(execute, canExecute ?? (_ => true));
        }

        protected Command<T> CreateAsyncCommand<T>(Func<T, Task> execute, Func<T, bool>? canExecute = null)
        {
            return new Command<T>(async (parameter) =>
            {
                try
                {
                    await execute(parameter);
                }
                catch (Exception ex)
                {
                    Logger?.LogError(ex, "Error in async command with parameter");
                    HandleError(ex);
                }
            }, canExecute ?? (_ => true));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                Logger?.LogDebug("Disposing {ViewModelType}", GetType().Name);
                _disposed = true;
            }
        }

        protected void ThrowIfDisposed()
        {
            ObjectDisposedException.ThrowIf(_disposed, this);
        }
    }
}
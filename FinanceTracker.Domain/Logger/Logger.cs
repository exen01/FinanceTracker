namespace FinanceTracker.Domain.Logger;

/// <summary>
/// Отвечает за запись логов.
/// </summary>
public class Logger
{
  #region Поля и свойства

  /// <summary>
  /// Путь до файла с логами.
  /// </summary>
  private readonly string _logFilePath;

  #endregion

  #region Методы

  /// <summary>
  /// Создает директорию и файл для логов.
  /// </summary>
  private void CreateLogFile()
  {
    try
    {
      var logDirectory = Path.GetDirectoryName(_logFilePath);

      if (!Directory.Exists(logDirectory))
      {
        Directory.CreateDirectory(logDirectory);
      }

      if (!File.Exists(_logFilePath))
      {
        using (File.Create(_logFilePath))
        {
        }
      }
    }
    catch (Exception e)
    {
      Console.WriteLine($"Не удалось создать лог-файл: {e.Message}");
    }
  }

  /// <summary>
  /// Запись сообщения в лог.
  /// </summary>
  /// <param name="message">Текст сообщения</param>
  public async Task LogAsync(string message)
  {
    var logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}\n";

    await File.AppendAllTextAsync(_logFilePath, logEntry);
  }

  #endregion

  #region Конструкторы

  /// <summary>
  /// Конструктор.
  /// </summary>
  /// <param name="logFilePath">Путь до файла с логами</param>
  public Logger(string logFilePath)
  {
    _logFilePath = logFilePath;

    CreateLogFile();
  }

  #endregion
}
namespace PolpAbp.Framework.Settings
{
    /// <summary>
    /// Provides the setting transformations 
    /// across the application. 
    /// By defining an applicaiton specific implementation, 
    /// the application may have its own expected permission 
    /// names.
    /// The packages include an identity mappping function 
    /// as the default implementation.
    /// </summary>
    public interface ISettingConvertor
    {
        /// <summary>
        /// Converts the given setting name 
        /// into one that is used internally. 
        /// </summary>
        /// <param name="input">Typically a client-oriented setting name</param>
        /// <returns>Internal setting name</returns>
        string Decode(string input);

    }
}

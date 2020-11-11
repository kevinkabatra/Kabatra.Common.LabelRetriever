namespace Kabatra.Common.LabelRetriever
{
    using Cultures;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Threading;

    /// <summary>
    ///     Supports getting labels from a language specific resource file.
    /// </summary>
    /// <seealso href="https://refactoring.guru/design-patterns/singleton"/>
    /// <remarks>Singleton class.</remarks>
    [SuppressMessage("Microsoft.Performance", "CA1822", Justification = "Instance determines translation")]
    public class LabelRetriever
    {
        private static LabelRetriever _retriever;
        private static readonly object ThreadSafeLock = new object();

        /// <summary>
        ///     Returns a singleton instance of <c>LabelRetriever</c>.
        /// </summary>
        /// <param name="language"></param>
        /// <returns><c>LabelRetriever</c></returns>
        /// <seealso href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/lock-statement"/>
        public static LabelRetriever GetLabelRetriever(Languages language = null)
        {
            if (_retriever == null)
            {
                // lock will prevent multiple threads from attempting to create the singleton.
                lock (ThreadSafeLock)
                {
                    // should check conditional again, after lock is released the singleton would have been creates=d during a previous thread.
                    if (_retriever == null)
                    {
                        _retriever = new LabelRetriever(language);
                    }
                }
            }

            return _retriever;
        }

        /// <summary>
        ///     Resets the singleton, this is required for unit testing.
        /// </summary>
        public static void Reset()
        {
            lock (ThreadSafeLock)
            {
                _retriever = null;
            }
        }

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="language">The language that will be used for translation purposes.</param>
        /// <remarks>If language is left null, will default to English United States.</remarks>
        private LabelRetriever(Languages language)
        {
            var languageValue = (language ?? Languages.EnglishUnitedStates).Value;
            var cultureInfo = CultureInfo.CreateSpecificCulture(languageValue);
            SetCultureTo(cultureInfo);
        }

        /// <summary>
        ///     Sets the Culture for the current thread.
        /// </summary>
        /// <param name="culture">Which culture are we using for our translations.</param>
        private static void SetCultureTo(CultureInfo culture)
        {
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }
    }
}

using System;

using System.Windows.Input;

namespace WpfTreeView
{
    /// <summary>
    /// A basic command that runs an Action
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Private Members

        /// <summary>
        /// The action to run
        /// </summary>
        private Action mAction;

        #endregion

        #region Public Events

        /// <summary>
        /// The event that is fired when the <see cref="CanExecute(object)"/> value has changed
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        #endregion
        
        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public RelayCommand(Action action)
        {
            mAction = action;
        }

        #endregion

        #region Command Methods
        /// <summary>
        /// A relay command can always execute. (In case of fale, the item on the Ui would be greied out)
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter) => true;
        //{
        //    return true;
        //}

        /// <summary>
        /// Executes the commands Action (received in the constructor)
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            mAction();
        }

        #endregion
    }
}

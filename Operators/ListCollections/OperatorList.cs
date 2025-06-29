using System.Collections.Generic;

namespace MiniUI.Operators
{
    internal class OperatorList
    {
        #region Private Fields
        
        private readonly List<IOperator> _operators = new List<IOperator>();
        
        #endregion
        
        #region Initialization
        
        public void Register(IOperator instance)
        {
            _operators.Add(instance);
        }
        
        #endregion
        
        #region OperatorList Logic
        
        public void InitializeOperatorsData()
        {
            foreach (var op in _operators)
            {
                op.Initialize();
                
                op.Subscribe();
            }
        }
        
        #endregion
    }
}
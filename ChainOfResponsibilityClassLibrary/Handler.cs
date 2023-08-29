namespace ChainOfResponsibilityClassLibrary
{
    public abstract class Handler
    {
        protected Handler successor;

        public void SetSuccessor(Handler successor)
        {
            this.successor = successor;
        }

        public abstract Task HandleRequest(OperationTypes.OPERATIONS operation);
    }
}

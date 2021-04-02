using GraphQL;
using GraphQL.Execution;
using GraphQL.Language.AST;
using System;

namespace Web.GraphQL
{
    public class SerialDocumentExecuter : DocumentExecuter
    {
        private static IExecutionStrategy ParallelExecutionStrategy = new ParallelExecutionStrategy();
        private static IExecutionStrategy SerialExecutionStrategy = new SerialExecutionStrategy();
        private static IExecutionStrategy SubscriptionExecutionStrategy = new SubscriptionExecutionStrategy();

        protected override IExecutionStrategy SelectExecutionStrategy(ExecutionContext context)
        {
            return context.Operation.OperationType switch
            {
                OperationType.Query => SerialExecutionStrategy,
                OperationType.Mutation => SerialExecutionStrategy,
                OperationType.Subscription => SubscriptionExecutionStrategy,
                _ => throw new InvalidOperationException($"Unexpected OperationType {context.Operation.OperationType}"),
            };
        }
    }
}
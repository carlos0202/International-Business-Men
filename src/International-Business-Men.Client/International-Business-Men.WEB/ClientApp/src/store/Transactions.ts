import { Action, Reducer } from 'redux';
import { AppThunkAction } from './';
import { ResponseModel } from './CurrencyRates';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface TransactionsState {
    isLoading: boolean;
    sku?: string;
    transactions: Transaction[];
}

export interface Transaction {
    sku: string;
    currency: string;
    amount: number;
}


// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestTransactionsAction {
    type: 'REQUEST_TRANSACTIONS';
    sku?: string;
}

interface ReceiveTransactionsAction {
    type: 'RECEIVE_TRANSACTIONS';
    sku?: string;
    transactions: Transaction[];
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestTransactionsAction | ReceiveTransactionsAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestTransactions: (sku: string): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        console.log(sku, appState, appState.transactions);
        if (appState && appState.transactions && sku !== appState.transactions.sku) {
            let route = sku.length ? `/Transactions/${sku}` : '/Transactions';
            fetch(`${process.env.REACT_APP_API_URL}${route}`)
                .then(response => response.json() as Promise<ResponseModel<Transaction>>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_TRANSACTIONS', transactions: data.result });
                });

            dispatch({ type: 'REQUEST_TRANSACTIONS' });
        }
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: TransactionsState = { transactions: [], isLoading: false };

export const reducer: Reducer<TransactionsState> = (state: TransactionsState | undefined, incomingAction: Action): TransactionsState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_TRANSACTIONS':
            return {
                transactions: state.transactions,
                isLoading: true
            };
        case 'RECEIVE_TRANSACTIONS':
            if (action.transactions) {
                return {
                    transactions: action.transactions,
                    isLoading: false
                };
            }
    }

    return state;
};

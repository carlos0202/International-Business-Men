import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { MemoryRouter } from 'react-router-dom';
import LoadingSpinner from './LoadingSpinner';

it('renders LoadingSpinner component witouth crashing', () => {
    const storeFake = (state: any) => ({
        default: () => { },
        subscribe: () => { },
        dispatch: () => { },
        getState: () => ({ ...state })
    });
    const store = storeFake({}) as any;

    ReactDOM.render(
        <Provider store={store}>
            <MemoryRouter>
                <LoadingSpinner />
            </MemoryRouter>
        </Provider>, document.createElement('div'));
});

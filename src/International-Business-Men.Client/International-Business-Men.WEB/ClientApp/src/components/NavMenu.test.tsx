﻿import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { MemoryRouter } from 'react-router-dom';
import NavMenu from './NavMenu';

it('renders NavMenu component witouth crashing', () => {
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
                <NavMenu />
            </MemoryRouter>
        </Provider>, document.createElement('div'));
});

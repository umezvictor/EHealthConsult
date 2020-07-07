import {createStore, applyMiddleware} from 'redux';
import thunk from 'redux-thunk';
import rootReducer from './reducers/index';

const initialState = {};

//array of middlewares -- only thunk in this case
const middleware = [thunk];

const store = createStore(
    rootReducer, 
    initialState, 
        applyMiddleware(...middleware)   
    );

export default store;


/**
 * const store = createStore(
    rootReducer, 
    initialState, 
    compose(
        applyMiddleware(...middleware),
        window.__REDUX_DEVTOOLS_EXTENSION__ && window.__REDUX_DEVTOOLS_EXTENSION__()
    )
    );
 */
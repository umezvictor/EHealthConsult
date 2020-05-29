//root reducers -- combine all reducers here
import {combineReducers} from 'redux';
import authReducer from './authReducer';

export default combineReducers({
    auth: authReducer, //accessible via this.props.auth
    //error: errorReducer,
    //customers: patientsReducer
});
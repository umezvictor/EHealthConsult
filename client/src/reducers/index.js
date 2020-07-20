//root reducers -- combine all reducers here
import {combineReducers} from 'redux';
import authReducer from './authReducer';
import appointmentReducer from './appointmentReducer';
import consultantReducer from './consultantReducer';

export default combineReducers({
    auth: authReducer, //accessible via state.auth
    appointment: appointmentReducer,
    consultant: consultantReducer
});
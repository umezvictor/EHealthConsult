import axios from 'axios';
import setAuthToken from '../utils/setAuthToken';
import jwt_decode from 'jwt-decode';

import { SET_CURRENT_USER, GET_ERRORS} from './types';

//handles signup
export const signupUser = (userData, history) => dispatch => {
    axios.post('http://localhost:5000/api/auth/signup', userData)
    .then(res => history.push('/login'))
    .catch(err => 
        dispatch({
            type: GET_ERRORS,
            payload: err.response
        })
        )
};

//handle login
export const loginUser = (userCredentials) => dispatch => {
    axios.post('http://localhost:5000/api/auth/login', userCredentials)
        .then(res => {
           
           const userToken = res.data.token;
            //save toen to localstorage
            
            localStorage.setItem('jwtToken', userToken);
            //set auth token to header
            setAuthToken(userToken);
            //decode token ti get usercredential
            const decodedToken = jwt_decode(userToken);
            dispatch(setCurrentUser(decodedToken));

            //console.log(token)
        })
        .catch(err => {
            dispatch({
                type: GET_ERRORS,
                paylod:err.response 
            })
        });
};



//set logged in user
export const setCurrentUser = (decodedToken) => {
    //dispatch to the reducer
    return {
        type: SET_CURRENT_USER,  //once dispatched, it is caught in the authReducer
        payload: decodedToken//contains user info that was decoded from the token
    }
};

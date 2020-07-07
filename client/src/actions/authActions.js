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
            //decode token to get user credentials
            const decodedToken = jwt_decode(userToken);

            dispatch(setCurrentUser(decodedToken));
           
        })
        .catch(err => {
            dispatch({
                type: GET_ERRORS,
                paylod:err.response 
            })
        });

};

//logout user
export const logoutUser = () => dispatch => {
    
    //remove token from localstorage
    localStorage.removeItem('jwtToken');
    //remove auth header for future request
    setAuthToken(false);
    //set current user to empty
    dispatch(setCurrentUser({}));
    //redirect to login page
    window.location.href = '/login';

}


//set logged in user
export const setCurrentUser = (decoded) => {
    //dispatch to the reducer
    return {
        type: SET_CURRENT_USER,  //once dispatched, it is caught in the authReducer
        payload: decoded//contains user info that was decoded from the token
    }
};



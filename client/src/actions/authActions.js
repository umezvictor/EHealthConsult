//Signup.js calls this action creator file
import axios from 'axios';
//import setAuthToken from '../utils/setAuthToken';
//import jwt_decode from 'jwt-decode';

import { SET_CURRENT_USER, GET_ERRORS} from './types';

//handles signup
export const signupUser = (userData) => dispatch => {

    //make api call here
    axios.post('http://localhost:5000/api/auth/signup', userData)
    .then(res => res.data)
    .catch(err => 
        dispatch({
            type: GET_ERRORS,
            payload: err.response.data,
        })
        )
};
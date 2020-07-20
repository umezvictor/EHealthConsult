import axios from 'axios';
import {ADD_CONSULTANT, GET_CONSULTANTS, GET_CONSULTANT, DELETE_APPOINTMENT, UPDATE_CONSULTANT, GET_ERRORS, GET_CONSULTANT_IMAGE, GET_CONSULTANTS_IMAGES} from './types';

/*
export const getConsultants = () => async dispatch => {
     const res = await axios.get('http://localhost:5000/api/consultants');
     dispatch({
        type: GET_CONSULTANTS,
        payload: res.data
    })
};
*/
//export default getConsultants;

 export const getConsultants = () => dispatch => {
     axios.get('http://localhost:5000/api/consultants')
        .then(res => 
                dispatch({
                    type: GET_CONSULTANTS,
                    payload: res.data
                })
            )
        .catch(err => {
            dispatch({
                type: GET_ERRORS
            })
        });
};
 
//add consultant
export const addConsultant = (cosultantData) => dispatch => {
    axios.post('http://localhost:5000/api/consultants', cosultantData)
       .then(res => 
               dispatch({
                   type: ADD_CONSULTANT,
                   payload: res.data
               })
           )
       .catch(err => {
           dispatch({
               type: GET_ERRORS
           })
       })
};
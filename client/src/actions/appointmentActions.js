import axios from 'axios';
import {BOOK_APPOINTMENT, GET_ERRORS} from './types';

export const bookAppointment = (appointmentDetails, consultantId, history) => dispatch => {
    axios.post(`http://localhost:5000/api/appointments/${consultantId}`, appointmentDetails)
        .then(res =>           
            dispatch({
                type: BOOK_APPOINTMENT,
                payload: res.data
            })            
        )
        .then(history.push('/payment'))
        .catch(err => {
            dispatch({
                type: GET_ERRORS
            })
        })
};




/**
 * flow 
 * user clicks on book appointment
 * user is redirected to payment page
 * on successful payment, user is redirected to book appointment form
 * payment status is captured
 * appointment is saved
 *  dispatch({
                type: BOOK_APPOINTMENT,
                payload: res.
            })
 */
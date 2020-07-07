import isEmpty from '../validation/is-empty';
import {BOOK_APPOINTMENT} from '../actions/types';



//reducer defines how the application state should change in response to an action


//initial state of appointments will be an empty array
const initialState = {
    appointment: {}
}
export default function(state = initialState, action){
    //console.log(action.payload);
    switch(action.type){
        case BOOK_APPOINTMENT: //uses this to identify the action that took place
            return{
                ...state, //return current state
                appointment: action.payload              
            }
        default:
            return state; 
    }
}
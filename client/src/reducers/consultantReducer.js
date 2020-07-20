import {GET_CONSULTANTS, GET_CONSULTANTS_IMAGES, ADD_CONSULTANT} from '../actions/types';

const initialState = {
    consultant: {},//holds single consultant
    consultants: [],//holds all consultants
    loading: false,
}

export default function(state = initialState, action) {
   console.log(action.payload);
    switch(action.type){
        case GET_CONSULTANTS:
            return {
                ...state,
                consultants: action.payload               
            } 
        case ADD_CONSULTANT: 
            return {
                ...state,
                consultant: action.payload
            }
        default: 
            return state;     
    }
}
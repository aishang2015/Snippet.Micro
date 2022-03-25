import { configureStore } from "@reduxjs/toolkit";
import CounterSliceReducer from "./slices/counter";


export default configureStore({
    reducer: {
        counterReducer: CounterSliceReducer
    }
});

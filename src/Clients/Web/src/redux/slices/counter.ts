import { createSlice } from "@reduxjs/toolkit";

export const CounterSlice = createSlice({
    name: 'counter',
    initialState: {
        count: 1,
        title: 'redux toolkit pre'
    },
    reducers: {
        increment(state, { payload }) {
            state.count += payload.step;
        },
        decrement(state) {
            state.count -= 1;
        }
    }
});

export const { increment, decrement } = CounterSlice.actions;
export default CounterSlice.reducer;
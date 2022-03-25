import { useDispatch, useSelector } from "react-redux";
import { decrement, increment } from "../../redux/slices/counter";


export function HomePage() {

    const { count } = useSelector((state: any) => {
        return state.counterReducer;
    });
    const dispatch = useDispatch();

    return (
        <>
            <p>this is a home page</p>
            <div>{count}</div>
            <button onClick={() => dispatch(increment({ step: 2 }))}>add</button>
            <button onClick={() => dispatch(decrement())}>sub</button>
        </>
    );
}
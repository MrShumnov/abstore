// import {Text} from 'react-native'

export default function MyText(props: any)
{
    return (
        <div style={{fontFamily: "consolas"}}>
            { props.children }
        </div>
    )
}
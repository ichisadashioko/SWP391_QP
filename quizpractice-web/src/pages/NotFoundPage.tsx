import { Button, Result } from 'antd'
import { Link } from 'react-router'

export function NotFoundPage() {
  return (
    <Result
      status="404"
      title="This page does not exist."
      extra={
        <Link to="/">
          <Button type="primary">Home Page</Button>
        </Link>
      }
    />
  )
}

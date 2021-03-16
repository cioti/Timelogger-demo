import axios, { Method } from 'axios'
const BASE_URL = 'http://localhost:3001';

class ApiClient {
	static async sendRequestAsync<TRequest, TResponse>(
		url: string,
		method: Method,
		body: TRequest): Promise<TResponse | null> {
		try {
			const response = await axios({
				baseURL: BASE_URL,
				url,
				method,
				data: body,
			});
			console.log(`Response status: ${response.status}`);
			console.log(`Response data: ${String(response.data)}`);
			const data: TResponse = response.data;
			return data;
		} catch (error) {
			console.log(error);
			return null;
		}
	}
}

export { ApiClient }
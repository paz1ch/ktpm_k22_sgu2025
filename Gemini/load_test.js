
import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
  stages: [
    { duration: '30s', target: 20 },
    { duration: '1m', target: 20 },
    { duration: '10s', target: 0 },
  ],
  thresholds: {
    http_req_duration: ['p(95)<500'], 
    http_req_failed: ['rate<0.01'],   
  },
};

const BASE_URL = 'http://localhost:8088';

export default function () {
  const resIndex = http.get(`${BASE_URL}/`);

  check(resIndex, {
    'Homepage status is 200': (r) => r.status === 200,
    'Homepage verify title': (r) => r.body.includes('DOCTYPE html'),
  });

  const resLang = http.get(`${BASE_URL}/Home/GetLanguage`);

  check(resLang, {
    'GetLanguage status is 200': (r) => r.status === 200,
    'Language is JSON': (r) => r.headers['Content-Type'] && r.headers['Content-Type'].includes('application/json'),
  });

  sleep(1);
}

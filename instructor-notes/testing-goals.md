# Testing Goals

The goal of CI/CD and all that flows from that (microservices, containers, k8s, all that) is TEAM AUTONOMY.

We make it so that a team is responsible for a business (or technical) requirement within our business. They *own* this and they are able to elevate new stuff *without coordination with other teams*. At least daily is the goal.

Our tests, then, have to verify these two things first and foremost (though not exclusively):

1. Does our stuff meet the business requirements? Does it "work" and do it "right"?
2. Are we *adhering to the published contract other apps use to communicate with our service*?
    - We do this with our integration tests.
    - Our integration tests are a mirror of how we expect users to use our API.
    


## In HTTP the "Interface" is

- The resources (the URLS)
- The representations (the data you send to or receive from the API)